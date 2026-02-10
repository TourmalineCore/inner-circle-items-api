
using Application.Commands;

namespace Application.Features.ItemTypes.CreateItemType;

public class CreateItemTypeHandler
{
    private readonly CreateItemTypeCommand _createItemTypeCommand;

    public CreateItemTypeHandler(
        CreateItemTypeCommand createItemTypeCommand
    )
    {
        _createItemTypeCommand = createItemTypeCommand;
    }

    public async Task<CreateItemTypeResponse> HandleAsync(CreateItemTypeRequest createItemTypeRequest)
    {
        var newItemTypeId = await _createItemTypeCommand.ExecuteAsync(createItemTypeRequest);

        return new CreateItemTypeResponse()
        {
            NewItemTypeId = newItemTypeId
        };
    }
}
