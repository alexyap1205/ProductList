'use strict';

productSummary.component('productSummary', {
    templateUrl: '/Modules/product-summary.template.html',
    bindings: {
        resolve: '<',
        close: '&',
        dismiss: '&'
    },
    controller: function () {
        this.item = {};

        this.$onInit = function () {
            this.item = this.resolve.item;
        };

        this.ok = function () {
            this.close({ $value: 'ok' });
        };

        this.cancel = function () {
            this.dismiss({ $value: 'cancel' });
        };
    }
});