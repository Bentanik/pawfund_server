﻿namespace PawFund.Contract.DTOs.PaymentDTOs;

public class CreatePaymentDTO
{
    public int OrderCode { get; set; }
    public string Description { get; set; }
    public List<ItemDTO> Items { get; set; }
    public string CancelUrl { get; set; }
    public string ReturnUrl { get; set; }

    public CreatePaymentDTO(int orderCode, string description, List<ItemDTO> items, string cancelUrl, string returnUrl)
    {
        OrderCode = orderCode;
        Description = description;
        Items = items;
        CancelUrl = cancelUrl;
        ReturnUrl = returnUrl;
    }
}