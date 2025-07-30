Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* header Content-Type = 'application/json'

Scenario: CRUD operations test flow

    * def jsUtils = read('../jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_LOGIN_WITH_ALL_PERMISSIONS')
    * def authPassword = jsUtils().getEnvVariable('AUTH_PASSWORD_WITH_ALL_PERMISSIONS')
    
    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And request
    """
    {
        "login": #(authLogin),
        "password": #(authPassword)
    }
    """
    And method POST
    Then status 200

    * def accessToken = karate.toMap(response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(accessToken)

    # Step 1: Create a new item type
    * def randomName = '[API-E2E]-Test-item-type-' + Math.random()
    
    Given url apiRootUrl
    Given path 'item-types'
    And request
    """
    {
        "name": "#(randomName)"
    }
    """
    When method POST
    Then status 200

    * def newItemTypeId = response.newItemTypeId

    # Step 2: Verify that item type is in the list with the id and generated name
    Given url apiRootUrl
    Given path 'item-types'
    When method GET
    And match response.itemTypes contains
    """
    {
        "id": "#(newItemTypeId)",
        "name": "#(randomName)"
    }
    """

    # Cleanup: Delete the item type (hard delete)
    Given path 'item-types', newItemTypeId, 'hard-delete'
    When method DELETE
    Then status 200
    And match response == { isDeleted: true }
