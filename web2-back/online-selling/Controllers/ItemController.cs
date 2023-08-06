using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_selling.Dto;
using online_selling.Interfaces.Items;
using online_selling.Models;

namespace online_selling.Controllers
{
    [Route("item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] AddItemDto addItemDto)
        {
            try
            {
                var result = await _itemService.AddItem(addItemDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDto item)
        {
            try
            {
                var result = await _itemService.UpdateItem(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteItems([FromBody] List<int> items)
        {
            try
            {
                await _itemService.DeleteItems(items);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetItemsByUserId([FromBody] UserIdDto idDto)
        {
            try
            {
                var result = await _itemService.GetItemsByUserId(idDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("getById")]
        public async Task<IActionResult> GetItemById([FromBody] int id)
        {
            try
            {
                var result = await _itemService.GetItemById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var result = await _itemService.GetAllItems();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
