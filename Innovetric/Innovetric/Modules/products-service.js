'use strict';

var productsServiceModule = angular.module('productsServiceModule', ['ngRoute']);

productsServiceModule.factory('productsService', function ($http, $q) {
    var self = this;

    var service = {};

    service.categories = {};
    service.productsByCategory = {};
    service.brands = {};
    service.productsByBrand = {};
    service.products = {};
    service.featuredProducts = [];
    service.isInitialized = false;

    service.initialize = function () {
        var deferred = $q.defer();

        deferred.notify('Getting products...');

        $http.get('/api/products')
            .success(function(data) {
                deferred.notify('Products Retrieved...');

                var i;

                service.featuredProducts = [];

                for (i = 0; i < data.length; i++) {
                    var product = data[i];
                    //service.products.push(product);

                    service.products[product.ID] = product;

                    if (product.Featured) {
                        service.featuredProducts.push(product);
                    }

                    var category = data[i].Category;

                    if (service.categories[category.ID] == undefined) {
                        service.categories[category.ID] = category;
                    }

                    if (service.productsByCategory[category.ID] == undefined) {
                        service.productsByCategory[category.ID] = [];
                    }
                    service.productsByCategory[category.ID].push(product);

                    var brand = data[i].Brand;

                    if (service.brands[brand.ID] == undefined) {
                        service.brands[brand.ID] = brand;
                    }

                    if (service.productsByBrand[brand.ID] == undefined) {
                        service.productsByBrand[brand.ID] = [];
                    }
                    service.productsByBrand[brand.ID].push(product);

                }

                service.isInitialized = true;

                deferred.resolve(service);
            })
            .error(function(data, status) {
                deferred.reject('Error: ' + status + ": data");
            });

        return deferred.promise;
    };


    service.getCategories = function () {
        var categoryList = [];

        for (var property in service.categories) {
            if (service.categories.hasOwnProperty(property)) {
                categoryList.push(service.categories[property]);
            }
        }

        return categoryList;
    };

    service.getBrands = function () {
        var brandList = [];

        for (var property in service.brands) {
            if (service.brands.hasOwnProperty(property)) {
                brandList.push(service.brands[property]);
            }
        }

        return brandList;
    };

    service.getFeaturedProducts = function() {
        return service.featuredProducts;
    }

    service.getProducts = function () {
        var productList = [];

        for (var property in service.products) {
            if (service.products.hasOwnProperty(property)) {
                productList.push(service.products[property]);
            }
        }

        return productList;
    };

    service.getProductById = function(productId) {
        if (service.products[productId] != undefined) {
            return service.products[productId];
        } else {
            throw "Product not found";
        }
    }

    service.getProductsByBrand = function (brandID) {
        if (service.productsByBrand[brandID] != undefined) {
            return service.productsByBrand[brandID];
        } else {
            throw "Brand is not found";
        }
    }

    service.getProductsByCategory = function(categoryID) {
        if (service.productsByCategory[categoryID] != undefined) {
            return service.productsByCategory[categoryID];
        } else {
            throw "Category is not found";
        }
    }

    service.getBrand = function(brandID) {
        if (service.brands[brandID] != undefined) {
            return service.brands[brandID];
        } else {
            throw "Brand is not found";
        }
    }

    service.getCategory = function (categoryID) {
        if (service.categories[categoryID] != undefined) {
            return service.categories[categoryID];
        } else {
            throw "Category is not found";
        }
    }

    return service;
})