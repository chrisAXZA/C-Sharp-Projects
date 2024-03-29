﻿namespace OnlineShop.Models.Products.Computers
{
    using System;

    public class DesktopComputer : Computer
    {
        private const double OVERALL_PERFORMANCE = 15;

        public DesktopComputer(int id, string manufacturer, string model, decimal price)
            : base(id, manufacturer, model, price, OVERALL_PERFORMANCE)
        {
        }
    }
}
