using GeneratorHelper;
using Microsoft.AspNetCore.Components;

namespace Blazortastic.Data;

public partial class TestobjectjeComponent_Overview_g
{
    [Inject] ICrudService<Testobjectje> Service { get; set; }
    private Testobjectje[]? TestobjectjeList { get; set; }
    protected override async Task OnInitializedAsync()
    {
        TestobjectjeList = await Service.GetAsync();
    }
}
