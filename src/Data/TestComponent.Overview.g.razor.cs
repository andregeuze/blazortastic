using GeneratorHelper;
using Microsoft.AspNetCore.Components;

namespace Blazortastic.Data;

public partial class TestComponent_Overview_g
{
    [Inject] ICrudService<Test> Service { get; set; }
    private Test[]? TestList { get; set; }
    protected override async Task OnInitializedAsync()
    {
        TestList = await Service.GetAsync();
    }
}
