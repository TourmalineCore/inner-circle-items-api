Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* header Content-Type = 'application/json'

Scenario: CRUD operations test flow

    * def jsUtils = read('../jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_LOGIN_WITHOUT_PERMS')
    * def authPassword = jsUtils().getEnvVariable('AUTH_PASSWORD_WITHOUT_PERMS')
    
    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And print jsUtils().getEnvVariable('AUTH_PASSWORD_WITHOUT_PERMS')
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

    # Step 1: Try to create a new item type
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
    Then status 403

    # Step 2: Try to get list of item types
    Given url apiRootUrl
    Given path 'item-types'
    When method GET
    Then status 403


    # Cleanup: Try to delete the item type (hard delete)
    Given path 'item-types', 1, 'hard-delete'
    When method DELETE
    Then status 403

