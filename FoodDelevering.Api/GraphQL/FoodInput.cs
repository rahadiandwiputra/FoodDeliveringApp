﻿namespace FoodDelevering.Api.GraphQL
{
    public record FoodInput
    (
        int? Id,
        string Name,
        int Stock,
        double Price,
        int CategoryId
    );
}
