'use strict';

productList.component('productList',
        {
            templateUrl: '/Modules/product-list.template.html',
            controller: ['productsService', '$routeParams', '$uibModal', function ProductListController(productsService, $routeParams, $uibModal) {
                this.selectionType = $routeParams.selection;
                this.selectionId = $routeParams.selectionId;

                this.typeName = "";
                this.selectionName = "";

                this.brands = {};
                this.categories = {};
                this.products = [];

                // Get brands and categories
                if (productsService.isInitialized) {
                    this.categories = productsService.getCategories();
                    this.brands = productsService.getBrands();
                } else {
                    var self = this;

                    productsService.initialize().then(function (service) {
                        self.categories = productsService.getCategories();
                        self.brands = productsService.getBrands();
                    }, function (reason) {
                        console.error(reason);
                    }, function (update) {
                        console.debug(update);
                    });
                }

                // Get products based on selection
                if (this.selectionType === 'brands') {
                    this.typeName = "Brand";
                    this.selectionName = productsService.getBrand(this.selectionId).Name;
                    this.products = productsService.getProductsByBrand(this.selectionId);
                } else {
                    this.typeName = "Category";
                    this.selectionName = productsService.getCategory(this.selectionId).Name;
                    this.products = productsService.getProductsByCategory(this.selectionId);
                }

                // Modal component related
                this.openModalComponent = function (productId) {
                    var modalInstance = $uibModal.open({
                        animation: true,
                        component: 'productSummary',
                        resolve: {
                            item: function () {
                                return productsService.getProductById(productId);
                            }
                        }
                    });

                    //modalInstance.result.then(function (selectedItem) {
                    //    $ctrl.selected = selectedItem;
                    //}, function () {
                    //    $log.info('modal-component dismissed at: ' + new Date());
                    //});
                };
            }]
        }
    );

