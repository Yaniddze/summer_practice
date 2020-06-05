using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TestApi.UseCases.ActivateAccount
{
    public class ActivateRequest: IRequest<ActivateAnswer>
    {
        [Required]
        public Guid Url { get; set; }
    }
}