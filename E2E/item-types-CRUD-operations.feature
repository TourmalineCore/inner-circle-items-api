Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* header Content-Type = 'application/json'

Scenario: CRUD operations test flow

    * def jsUtils = read('../jsUtils.js')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')

    Given url apiRootUrl
    Given path 'api/item-types'
    When method GET
    Then status 200
