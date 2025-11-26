using Application.Commands;

namespace Api.Features.Items.CreateItem;

public class CreateItemHandler
{
    private readonly CreateItemCommand _createItemCommand;

    public CreateItemHandler(CreateItemCommand createItemCommand)
    {
        _createItemCommand = createItemCommand;
    }

    public async Task<CreateItemResponse> HandleAsync(CreateItemRequest createItemRequest)
    {
        var createItemCommandParams = new CreateItemCommandParams
        {
            Name = createItemRequest.Name,
            SerialNumber = createItemRequest.SerialNumber,
            ItemTypeId = createItemRequest.ItemTypeId,
            Price = createItemRequest.Price,
            Description = createItemRequest.Description,
            PurchaseDate = createItemRequest.PurchaseDate,
            HolderEmployeeId = createItemRequest.HolderEmployeeId
        };

        var newItemId = await _createItemCommand.ExecuteAsync(createItemCommandParams);

        return new CreateItemResponse()
        {
            NewItemId = newItemId
        };
    }
}
