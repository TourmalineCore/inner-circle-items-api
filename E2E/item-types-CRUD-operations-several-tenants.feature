Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* header Content-Type = 'application/json'

Scenario: CRUD operations test flow

    * def jsUtils = read('../jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLoginFirstTenant = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_LOGIN_WITH_ALL_PERMISSIONS')
    * def authPasswordFirstTenant = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_PASSWORD_WITH_ALL_PERMISSIONS')
    * def authLoginSecondTenant = jsUtils().getEnvVariable('AUTH_SECOND_TENANT_LOGIN_WITH_ALL_PERMISSIONS') 
    * def authPasswordSecondTenant = jsUtils().getEnvVariable('AUTH_SECOND_TENANT_PASSWORD_WITH_ALL_PERMISSIONS')
    
    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And request
    """
    {
        "login": #(authLoginFirstTenant),
        "password": #(authPasswordFirstTenant)
    }
    """
    And method POST
    Then status 200

    * def accessTokenFirstTenant = karate.toMap(response.accessToken.value)

    Given url authApiRootUrl
    And path '/auth/login'
    And request
    """
    {
        "login": #(authLoginSecondTenant),
        "password": #(authPasswordSecondTenant)
    }
    """
    And method POST
    Then status 200

    * def accessTokenSecondTenant = karate.toMap(response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(accessTokenFirstTenant)

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

    * configure headers = jsUtils().getAuthHeaders(accessTokenSecondTenant)

    # Step 3: Try to get items generated with other tenant
    Given url apiRootUrl
    Given path 'item-types'
    When method GET
    Then status 200
    And match response.itemTypes == []

    # Step 4: Try to delete item from other tenant
    Given path 'item-types', newItemTypeId, 'hard-delete'
    When method DELETE
    Then status 500

    * configure headers = jsUtils().getAuthHeaders(accessTokenFirstTenant)

    # Cleanup: Delete the item type (hard delete)
    Given path 'item-types', newItemTypeId, 'hard-delete'
    When method DELETE
    Then status 200
    And match response == { isDeleted: true }
