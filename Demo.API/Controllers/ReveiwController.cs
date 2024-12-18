﻿

using Demo.Business.DTOs.Opportunity;
using Demo.Business.DTOs.Review;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }


    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<ReviewDto>> GetAll()
    {
        try
        {
            var reviews = _reviewService.GetAllReviews();
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    public ActionResult<ReviewDto> GetById(int id)
    {
        try
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound($"Review with ID {id} not found.");

            return Ok(review);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/details")]
    public ActionResult<ReviewWithAllDataDto> GetWithAllData(int id)
    {
        try
        {
            var review = _reviewService.GetReviewWithAllData(id);
            if (review == null)
                return NotFound($"Review with ID {id} not found.");

            return Ok(review);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/application")]
    public ActionResult<ReviewWithApplicationDto> GetWithApplication(int id)
    {
        try
        {
            var review = _reviewService.GetReviewWithApplication(id);
            if (review == null)
                return NotFound($"Review with ID {id} not found.");

            return Ok(review);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    //[Authorize]
    //[OrganizationFilter]
    public ActionResult<ReviewDto> Create([FromBody] CreateReviewDto createReviewDto)
    {
        //string tokenId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        //if (createReviewDto.OrganizationId != tokenId)
        //    return BadRequest($"Organization with ID: {tokenId} can't add review instead of organization with ID: {createReviewDto.OrganizationId}");

        try
        {
            if (createReviewDto == null)
                return BadRequest("Review data is null.");

            var createdReview = _reviewService.CreateReview(createReviewDto);
            return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    //[Authorize]
    //[OrganizationFilter]
    public ActionResult<ReviewDto> Update(int id, [FromBody] UpdateReviewDto updateReviewDto)
    {

        try
        {
            if (updateReviewDto == null)
                return BadRequest("Review data is null.");

            var updatedReview = _reviewService.UpdateReview(id, updateReviewDto);
            if (updatedReview == null)
                return NotFound($"Review with ID {id} not found.");

            return Ok(updatedReview);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    //[Authorize]
    //[OrganizationFilter]    
    public IActionResult Delete(int id)
    {
        try
        {
            var isDeleted = _reviewService.DeleteReview(id);
            if (!isDeleted)
                return NotFound($"Review with ID {id} not found.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
