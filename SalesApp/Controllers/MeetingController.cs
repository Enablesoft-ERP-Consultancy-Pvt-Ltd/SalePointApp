using SalesApp.Helper;
using SalesApp.Model;
using SalesApp.Model.Employee;
using SalesApp.Model.Meeting;
using SalesApp.ViewModel;
using SalesApp.ViewModel.Meeting;
using SalesApp.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Model;
using SalesApp.WebAPI.Service.IService;

namespace SalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService mtgSrv;
        private readonly ICommanService comSrv;

        public MeetingController(IMeetingService _mtgSrv, ICommanService _comSrv)
        {
            this.mtgSrv = _mtgSrv;
            this.comSrv = _comSrv;
        }

        [HttpPost("addMeeting")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewMeeting([FromBody] MeetingJson model)
        {
            try
            {
                MeetingModel obj = new MeetingModel()
                {
                    MeetingDate = model.MeetingDate.ParseDate(),
                    MeetingNote = model.MeetingNote,
                    ClientId = model.ClientId,
                    EmpId = model.EmpId,
                    StartTime = model.StartTime.ParseTime(),
                    EndTime = model.EndTime.ParseTime(),
                    EmpList = model.EmpList,
                    IsStatus = (short)MeetingEnum.Active,
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now
                };
                return Ok(await mtgSrv.AddMeeting(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("addRecurringMeeting")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRecurringMeeting([FromBody] MeetingJson model)
        {
            try
            {
                MeetingModel obj = new MeetingModel()
                {
                    MeetingDate = model.MeetingDate.ParseDate(),
                    MeetingNote = model.MeetingNote,
                    FromDate = model.FromDate.ParseDate(),
                    ToDate = model.ToDate.ParseDate(),
                    ClientId = model.ClientId,
                    EmpId = model.EmpId,
                    StartTime = model.StartTime.ParseTime(),
                    EndTime = model.EndTime.ParseTime(),
                    EmpList = model.EmpList,
                    IsStatus = (short)MeetingEnum.Active,
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now
                };
                return Ok(await mtgSrv.AddRecurringMeeting(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("getEmpMeeting/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpMeetingList(int empId)
        {
            return Ok(await mtgSrv.GetEmpMeetingList(empId));
        }





        [HttpGet("getUserMeeting/{userId}/{userTypeId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmpMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserMeetingList(int userId, short userTypeId)
        {
            return Ok(await mtgSrv.GetUserMeetingList(userId, userTypeId));
        }













        [HttpGet("getClientMeetingList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientMeetingList()
        {
            return Ok(await mtgSrv.GetClientMeetingList());
        }

        [HttpPost("getClientMeetingList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientMeeting>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientMeetingList([FromBody] ClientFilter model)
        {
            return Ok(await mtgSrv.GetClientMeetingList(model));
        }




        [HttpGet("getMeetingDetail/{meetingId}")]
        [ProducesResponseType(typeof(ServiceResponse<MeetingView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<MeetingView>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingDetail(long meetingId)
        {
            return Ok(await mtgSrv.GetMeetingDetail(meetingId));
        }


        [HttpPost("updateMeeting")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMeeting([FromBody] MeetingJson model)
        {
            try
            {
                MeetingModel obj = new MeetingModel()
                {
                    MeetingId = model.MeetingId,
                    MeetingDate = model.MeetingDate.ParseDate(),
                    MeetingNote = model.MeetingNote,
                    FromDate = model.FromDate.ParseDate(),
                    ToDate = model.ToDate.ParseDate(),
                    ClientId = model.ClientId,
                    EmpId = model.EmpId,
                    StartTime = model.StartTime.ParseTime(),
                    EndTime = model.EndTime.ParseTime(),
                    IsStatus = (short)MeetingEnum.Active,
                    CreatedBy = model.UserId,
                    CreatedOn = DateTime.Now
                };
                return Ok(await mtgSrv.UpdateMeeting(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("addNote")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNote([FromBody] NotesModel model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                return Ok(await mtgSrv.PostNote(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("updateStatus")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus([FromBody] MeetingStatus model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                return Ok(await mtgSrv.ChangeStatus(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("upCommingApp/{clientId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<MeetingView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<MeetingView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpCommingAppointments(int clientId)
        {
            return Ok(await mtgSrv.UpCommingAppointments(clientId));
        }



        [HttpGet("getMeetingLog/{meetingId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<MeetingLog>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<MeetingLog>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingLog(long meetingId)
        {
            return Ok(await mtgSrv.GetMeetingLog(meetingId));
        }



        [HttpPost("AddUpdateMeetingRate")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUpdateMeetingRate(MeetingRateModel model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                model.BillingStatus = (short)BillingStatus.Posted;
                return Ok(await mtgSrv.AddUpdateMeetingRate(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetMeetingRateByMeetingId/{MeetingId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<MeetingRateViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<MeetingRateViewModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingRateByMeetingId(long MeetingId)
        {
            try
            {
                return Ok(await mtgSrv.GetMeetingRateByMeetingId(MeetingId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
