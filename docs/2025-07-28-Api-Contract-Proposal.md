# API Contract Proposal

# Get All Item Types
GET /item-types
response
{
  "itemTypes": [{
    "id": long,
    "name": string
  }]
}

# Add new Item Types
POST /item-types
body
{
  "name": string
}
response
{
  "newItemTypeId": long
}  

# Delete Item Type
DELETE /item-types/{id}/hard-delete
response
{
  "isDeleted": bool
}

# Soft Delete Item Type
DELETE /item-types/{id}/soft-delete
response
{
  "isDeleted": bool
}


# Get All Items
GET /items
response
{
  "items": [{
    "id": long
    "name": string,
    "serialNumber": string | "",
    "itemType": {
      "id": long,
      "name": string
    },
    "price": decimal,
    // "purchaseDate": "2024",
    // "purchaseDate": "2025-07",
    "purchaseDate": DateOnly | null(Null Object?),
    "holderEmployee": {
      "id": long,
      "fullName": string
    } | null,
    // not in the first release for items crud
    // "status": Status
  }]
}
>Note: items-api depends on employees-api to add this name information. items-ui makes calls only yo items-api, it isn't calling employees-api directly. items-api loads items from db, make a network call to employees-api (not accessing its db directly) to retrieve all active(?) employees.

# Adding new item
POST /items
body
{
  "name": string,
  "serialNumber": string | "",
  "itemTypeId": long,
  "price": decimal,
  "purchaseDate": DateOnly | null(Null Object?),
  "holderEmployeeId": long | null
}
response
{
  "newItemId": long
}

# Hard Delete Item
DELETE /items/{id}/hard-delete
response
{
  "isDeleted": bool
}

------------------------ BELOW NOT APPROVED YET ----------------------------

# Create Delisted record
POST /delisted-item
body
{
  "itemId": long,
  "description": string
}
response
{
  "delistId": long
}

# Create a broken record
POST /broken-record
body
{
  "itemId": long,
  "description": string
}
response
{
  "brokenRecordId": long
}

# Update broken record (e.g. successfully fixed the issue)
PUT /broken-record/{id}
body
{
  "isFinished": bool,
  "description": string,
  "status": status
}
response
{
}

# Get ready to use Items
GET /items?status=0
body
{
}
response
{
  "items": List<Item>
}

# Get broken Items
GET /items?status=1
body
{
}
response
{
  "items": List<Item>
}

# Get delisted Items
GET /items?status=2
body
{
}
response
{
  "items": List<Item>
}

# Get items by holder id (Tourmaline, Free, Some worker)
GET /items?holderId={id}
body
{
}
response
{
  "items": List<Item>
}

# Update Item (e.g. reassign item to other person)
PUT /items/{id}/update
body
{
  "name": string:null,
  "serialNumber": string:null,
  "itemTypeId": long:null,
  "price": double:null,
  "purchaseDate": DateOnly:null,
  "holderId": long:null,
  "status": Status:null
}
response
{
}

# Soft Delete Item
DELETE /items/{id}
body
{
}
response
{
  "isDeleted": bool
}
