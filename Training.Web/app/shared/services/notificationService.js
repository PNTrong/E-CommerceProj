(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        function displaySuccess(message) {
            toastr.success(message);
        }

        function displayError(message) {
            if (Array.isArray(message)) {
                message.each(function (err) {
                    toastr.error(err);
                })
            } else {
                toastr.error(message);
            }
        }

        function displayWarning(message) {
            toastr.warning(message);
        }

        function displayInfo(message) {
            toastr.info(message);
        }

        return {
            Success: displaySuccess,
            Error: displayError,
            Warning: displayWarning,
            Info: displayInfo
        }
    }
})(angular.module('trainingshop.common'));