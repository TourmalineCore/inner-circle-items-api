Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* header Content-Type = 'application/json'

Scenario: CRUD operations test flow

    * def jsUtils = read('../jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_LOGIN_WITHOUT_PERMISSIONS')
    * def authPassword = jsUtils().getEnvVariable('AUTH_PASSWORD_WITHOUT_PERMISSIONS')
    
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

    Given url apiRootUrl
    Given path 'item-types'
    When method POST
    Then status 403

    Given url apiRootUrl
    Given path 'item-types'
    When method GET
    Then status 403

    Given path 'item-types', 100500, 'hard-delete'
    When method DELETE
    Then status 403
