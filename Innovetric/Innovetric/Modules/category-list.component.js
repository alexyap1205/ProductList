'use strict';

categoryList.component('categoryList',
        {
            templateUrl: '/Modules/category-list.template.html',
            controller: ['productsService', '$uibModal', function CategoryListController(productsService, $uibModal) {
                this.carouselInterval = 2000;
                this.active = 0;
                this.noWrapSlides = true;
                this.categories = {};
                this.brands = {};
                this.featuredProducts = {};

                if (productsService.isInitialized) {
                    this.categories = productsService.getCategories();
                    this.brands = productsService.getBrands();
                    this.featuredProducts = productsService.getFeaturedProducts();
                } else {
                    var self = this;

                    productsService.initialize().then(function(service) {
                        self.categories = productsService.getCategories();
                        self.brands = productsService.getBrands();
                        self.featuredProducts = productsService.getFeaturedProducts();
                    }, function(reason) {
                        console.error(reason);
                    }, function(update) {
                        console.debug(update);
                    });
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