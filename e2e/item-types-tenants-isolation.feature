Feature: Item Types
    # https://github.com/karatelabs/karate/issues/1191
    # https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

  Background:
    * header Content-Type = 'application/json'

  Scenario: Tenants Isolation

    * def jsUtils = read('./js-utils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authFirstTenantLogin = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_LOGIN_WITH_ALL_PERMISSIONS')
    * def authFirstTenantPassword = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_PASSWORD_WITH_ALL_PERMISSIONS')
    * def authSecondTenantLogin = jsUtils().getEnvVariable('AUTH_SECOND_TENANT_LOGIN_WITH_ALL_PERMISSIONS') 
    * def authSecondTenantPassword = jsUtils().getEnvVariable('AUTH_SECOND_TENANT_PASSWORD_WITH_ALL_PERMISSIONS')
    
    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And request
    """
    {
        "login": "#(authFirstTenantLogin)",
        "password": "#(authFirstTenantPassword)"
    }
    """
    And method POST
    Then status 200

    * def firstTenantAccessToken = karate.toMap(response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(firstTenantAccessToken)

    # Step 1: Create a new item type
    * def randomName = '[API-E2E]-Test-item-type-' + Math.random()
    
    Given url apiRootUrl
    Given path 'api/item-types'
    And request
    """
    {
        "name": "#(randomName)"
    }
    """
    When method POST
    Then status 200

    * def newItemTypeId = response.newItemTypeId

    # Authentication with other tenant
    Given url authApiRootUrl
    And path '/auth/login'
    And request
    """
    {
        "login": "#(authSecondTenantLogin)",
        "password": "#(authSecondTenantPassword)"
    }
    """
    And method POST
    Then status 200

    * def secondTenantAccessToken = karate.toMap(response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(secondTenantAccessToken)

    # Step 2: Cannot get item-types generated within another Tenant
    Given url apiRootUrl
    Given path 'api/item-types'
    When method GET
    Then status 200
    And match response.itemTypes == []

    # Step 3: Cannot delete item type of another Tenant
    Given path 'api/item-types', newItemTypeId, 'hard-delete'
    When method DELETE
    Then status 200
    And match response == { isDeleted: false }

    * configure headers = jsUtils().getAuthHeaders(firstTenantAccessToken)

    # Cleanup: Delete the item type (hard delete)
    Given path 'api/item-types', newItemTypeId, 'hard-delete'
    When method DELETE
    Then status 200
    And match response == { isDeleted: true }
