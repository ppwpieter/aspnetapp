using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetapp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        _logger.LogInformation("IndexModel constructor called at {time}, cool!", DateTime.UtcNow);
    }

    public void OnGet()
    {

    }
}
