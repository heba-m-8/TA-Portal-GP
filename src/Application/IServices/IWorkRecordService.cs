using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface IWorkRecordService
{
    public Task<WorkRecordDetailsDto> GetWorkRecordById(int workRecordId, int userId);

}
