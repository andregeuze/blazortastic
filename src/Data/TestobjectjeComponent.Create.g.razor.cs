using GeneratorHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazortastic.Data;

public partial class TestobjectjeComponent_Create_g
{
    [Inject] ICrudService<Testobjectje> Service { get; set; }
    Testobjectje model = new Testobjectje();
    bool success;
    private async Task OnValidSubmit(EditContext context)
    {
        success = true;
        await Service.Post(model);
    }
}
