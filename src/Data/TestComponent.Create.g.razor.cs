using GeneratorHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazortastic.Data;

public partial class TestComponent_Create_g
{
    [Inject] ICrudService<Test> Service { get; set; }
    Test model = new Test();
    bool success;
    private async Task OnValidSubmit(EditContext context)
    {
        success = true;
        await Service.Post(model);
    }
}
