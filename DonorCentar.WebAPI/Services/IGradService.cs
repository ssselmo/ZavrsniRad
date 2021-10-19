using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public interface IGradService: IReadService<Model.Grad, GradSearchRequest>
    {

    }
}
