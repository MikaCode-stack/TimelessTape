using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimelessTapes.Data;
using TimelessTapes.Models;

[Route("api/[controller]")]
[ApiController]
public class CsvUploadController : ControllerBase
{

    private readonly DBHandler _context;

    public CsvUploadController(DBHandler context)
    {
        _context = context; 
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload a valid CSV file.");
        }

        try
        {

           
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null
            }))
            {
                var records = csv.GetRecords<Title>().ToList();

                if (records == null || records.Count == 0)
                {
                    return BadRequest("CSV file is empty or could not be parsed.");
                }

                try
                {
                    await _context.Titles.AddRangeAsync(records);
                    // loop through and add entities
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // log this properly!
                    Console.WriteLine(ex.Message);
                }



                return Ok(new { Message = "CSV uploaded and processed successfully.", records.Count });
            }
        }
        catch (CsvHelperException ex)
        {
            return BadRequest($"CSV Parsing Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
