function fn() {
    return {

        shouldUseFakeExternalDependencies: function () {
            return this.getEnvVariable('SHOULD_USE_FAKE_EXTERNAL_DEPENDENCIES') === 'true';            
        },

        getEnvVariable: function (variable) {
            var System = Java.type('java.lang.System');

            return System.getenv(variable);
        },
    }
}