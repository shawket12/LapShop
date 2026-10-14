using AutoMapper;
using BusinessLayer.Bl.Interfaces;
using Domains.Models;
using LapShop.Dto;
using LapShop.Dto.Common;
using LapShop.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;

namespace LapShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemsApiController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
    int? categoryId,
    string? searchTerm
     ,[FromQuery] PaginationQuery pagination,
     bool sortDescending = false)
        {
            var sortBy = pagination.SortBy?.ToLowerInvariant();

            

            if (!string.IsNullOrWhiteSpace(pagination.SortBy) &&
                pagination.SortBy.ToLower() != "itemid" &&
                pagination.SortBy.ToLower() != "itemname" &&
                pagination.SortBy.ToLower() != "salesprice")
            {
                return BadRequest(
                    "Invalid sortBy value. Allowed values: ItemId, ItemName, SalesPrice.");
            }
            Expression<Func<TbItem, bool>>? predicate = null;

            if (categoryId.HasValue && !string.IsNullOrWhiteSpace(searchTerm))
            {
                predicate = i =>
                    i.CategoryId == categoryId.Value &&
                    i.ItemName.Contains(searchTerm);
            }
            else if (categoryId.HasValue)
            {
                predicate = i => i.CategoryId == categoryId.Value;
            }
            else if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                predicate = i => i.ItemName.Contains(searchTerm);
            }


            var result = sortBy switch
            {
                "salesprice" => await _unitOfWork.Items.GetPagedAsync(
                    predicate, i => i.SalesPrice,
                    pagination.PageNumber, pagination.PageSize,
                    sortDescending),

                "itemname" => await _unitOfWork.Items.GetPagedAsync(
                    predicate, i => i.ItemName,
                    pagination.PageNumber, pagination.PageSize,
                    sortDescending),

                _ => await _unitOfWork.Items.GetPagedAsync(
                    predicate, i => i.ItemId,
                    pagination.PageNumber, pagination.PageSize,
                    sortDescending)
            };

            var itemsDto = _mapper.Map<IEnumerable<ItemListDto>>(result.Items);
            var response = new PagedResult<ItemListDto>
            {
                Items = itemsDto,
                TotalCount = result.TotalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Category",
                    Detail = "The specified category does not exist."
                });
            }

            var item = _mapper.Map<TbItem>(dto);

            await _unitOfWork.Items.AddAsync(item);

            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.ItemId }, _mapper.Map<ItemDto>(item));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id);

            if (item == null)
            {
                throw new NotFoundException($"Item with id {id} was not found");
            }

            return Ok(_mapper.Map<ItemDto>(item));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateItemDto dto)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);

            if (category == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Category",
                    Detail = $"Category with id {dto.CategoryId} does not exist."
                });
            }

            var itemType = await _unitOfWork.ItemsType.GetByIdAsync(dto.ItemTypeId);

            if (itemType == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Item Type",
                    Detail = $"Item Type with id {dto.ItemTypeId} does not exist."
                });
            }

            var os = await _unitOfWork.OS.GetByIdAsync(dto.OsId);

            if (os == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Operating System",
                    Detail = $"Operating System with id {dto.OsId} does not exist."
                });
            }

            _mapper.Map(dto, item);

            _unitOfWork.Items.Update(item);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _unitOfWork.Items.Delete(item);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("Something went wrong");
        }


        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var item = await _unitOfWork.Items.GetByIdAsync(id);

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    var itemDto = _mapper.Map<ItemDto>(item);

        //    return Ok(itemDto);
        //}
    }
}
