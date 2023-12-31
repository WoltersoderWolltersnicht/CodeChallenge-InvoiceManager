﻿using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Domain.InvoiceLines;

public class InvoiceLine : Entity
{
    public uint? VAT { get; set; }
    public double? Amount { get; set; }
    public Invoice Invoice { get; set; }
}
