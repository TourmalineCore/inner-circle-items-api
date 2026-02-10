using Application.Commands;

namespace Application.Features.Items.CreateItem;

public class CreateItemHandler
{
    private readonly CreateItemCommand _createItemCommand;

    public CreateItemHandler(
        CreateItemCommand createItemCommand
    )
    {
        _createItemCommand = createItemCommand;
    }

    public async Task<CreateItemResponse> HandleAsync(CreateItemRequest createItemRequest)
    {
        return new CreateItemResponse
        {
            NewItemId = await _createItemCommand.ExecuteAsync(createItemRequest)
        };
    }
}
