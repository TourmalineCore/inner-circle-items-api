Feature: Item
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* header Content-Type = 'application/json'

Scenario: Happy Path

    * def jsUtils = read('../jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_LOGIN_WITH_ALL_PERMISSIONS')
    * def authPassword = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_PASSWORD_WITH_ALL_PERMISSIONS')

    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And request
    """
    {
        "login": "#(authLogin)",
        "password": "#(authPassword)"
    }
    """
    And method POST
    Then status 200

    * def accessToken = karate.toMap(response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(accessToken)

    # Step 1: Create a new item type
    * def itemTypeRandomName = '[API-E2E]-Test-item-type-' + Math.random()
    
    Given url apiRootUrl
    Given path 'item-types'
    And request
    """
    {
        "name": "#(itemTypeRandomName)"
    }
    """
    When method POST
    Then status 200

    * def newItemTypeId = response.newItemTypeId

    # Step 2: Create a new item
    * def itemRandomName = '[API-E2E]-Test-item' + Math.random()
    
    Given url apiRootUrl
    Given path 'items'
    And request
    """
    {
        "name": "#(itemRandomName)",
        "serialNumber": "123456/654321",
        "itemTypeId": "#(newItemTypeId)",
        "price": 322,
        "description": "#(itemRandomName)",
        "purchaseDate": "2025-07-31",
        "holderId": 555
    }
    """
    When method POST
    Then status 200

    * def newItemId = response.newItemId

    # Step 3: Verify that item is in the list with the id and generated name
    Given url apiRootUrl
    Given path 'items'
    When method GET
    And match response.items contains
    """
    {
        "id": "#(newItemId)",
        "name": "#(itemRandomName)",
        "serialNumber": "123456/654321",
        "itemType": {
            "id": "#(newItemTypeId)",
            "name": "#(itemTypeRandomName)"
        },
        "price": 322,
        "description": "#(itemRandomName)",
        "purchaseDate": "2025-07-31",
        "holderEmployee": null
    }
    """

    # Cleanup: Delete the item (hard delete)
    Given path 'items', newItemId, 'hard-delete'
    When method DELETE
    Then status 200
    And match response == { isDeleted: true }

    # Cleanup: Delete the item type (hard delete)
    Given path 'item-types', newItemTypeId, 'hard-delete'
    When method DELETE
    Then status 200
    And match response == { isDeleted: true }
