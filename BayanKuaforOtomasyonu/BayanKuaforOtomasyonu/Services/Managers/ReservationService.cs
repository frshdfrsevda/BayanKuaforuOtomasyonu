using Azure.Core;
using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationDetailRepository _reservationDetailRepository;
        private readonly IReservationStatusRepository _reservationStatusRepository;
        private readonly UserManager<AppUser> _userManager;

        public ReservationService(IReservationRepository reservationRepository, UserManager<AppUser> userManager, IReservationDetailRepository reservationDetailRepository, IReservationStatusRepository reservationStatusRepository)
        {
            _reservationRepository = reservationRepository;
            _userManager = userManager;
            _reservationDetailRepository = reservationDetailRepository;
            _reservationStatusRepository = reservationStatusRepository;
        }

        public void ApproveRes(int resid)
        {
            var resStatus = _reservationStatusRepository.GetByIdWithProps(x => x.ResId == resid);
            resStatus.isStatus=true;
            _reservationStatusRepository.Update(resStatus);
            _reservationStatusRepository.Save();
        }

        public (bool, string) ChangeReservationDate(ChangeDateResViewModel model)
        {
            var res = _reservationRepository.GetById(model.Id);

            if (!ControlReservation(model.ResDate, model.ResTime, res.TotalDuration))
                return (false, "Randevu başka bir randevuyla çakıştığı için güncellenemedi");

            res.ResDate = model.ResDate;
            res.ResTime = model.ResTime;
            _reservationRepository.Update(res);
            _reservationRepository.Save();
            return (true, "Randevu başarıyla güncellendi.");
        }

        public void ChangeStatusForManager(int resid)
        {
            var resStatus = _reservationStatusRepository.GetByIdWithProps(x=>x.ResId==resid);
            resStatus.isManagerEdit = true;
            resStatus.isUserEdit = false;
            _reservationStatusRepository.Update(resStatus);
            _reservationStatusRepository.Save();
        }
        public void ChangeStatusForUser(int resid)
        {
            var resStatus = _reservationStatusRepository.GetByIdWithProps(x => x.ResId == resid);
            resStatus.isManagerEdit = false;
            resStatus.isUserEdit = true;
            _reservationStatusRepository.Update(resStatus);
            _reservationStatusRepository.Save();
        }
        public bool ControlReservation(DateTime date, TimeSpan time, int totalDuration)
        {
            var modelEnd = time.Add(TimeSpan.FromMinutes(totalDuration));
            var resCtrlList = _reservationRepository.GetAllByCondition(x => x.ResDate == date);
            foreach (var resCtrl in resCtrlList)
            {
                var start = resCtrl.ResTime;
                var end = resCtrl.ResTime.Add(TimeSpan.FromMinutes(resCtrl.TotalDuration));
                if ((start <= time && time <= end) || (start <= modelEnd && modelEnd <= end))
                {
                    return false;

                }
            }
            return true;
        }

        public async Task CreateReservationAsync(AddReservationViewModel viewModel)
        {
            var currentUser = await _userManager.FindByNameAsync(viewModel.AppUserId);
            viewModel.AppUserId = currentUser.Id;
            var res = new Reservation()
            {
                AppUserId = viewModel.AppUserId,
                ResDate = viewModel.ResDate,
                ResTime = viewModel.ResTime,
                TotalDuration = viewModel.TotalDuration,
                TotalPrice = viewModel.TotalPrice
            };
            _reservationRepository.Add(res);
            _reservationRepository.Save();
            var userEmployments = new List<int>();
            foreach (var prop in viewModel.ReservationDetailIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                _reservationDetailRepository.Add(new ReservationDetail()
                {
                    ResId= res.Id,
                    AppUserEmploymentId=int.Parse(prop)
                });
            }
            _reservationDetailRepository.Save();
            _reservationStatusRepository.Add(new ReservationStatus() { 
                ResId = res.Id,
                isStatus =null,
                isManagerEdit = false,
                isUserEdit = false
            });
            _reservationStatusRepository.Save();
        }

        public void DeclineRes(int resid)
        {
            var resStatus = _reservationStatusRepository.GetByIdWithProps(x => x.ResId == resid);
            resStatus.isStatus = false;
            _reservationStatusRepository.Update(resStatus);
            _reservationStatusRepository.Save();
        }

        public List<ReservationViewModel> GetAllByStatus(bool? status)
        {
            var reservations = _reservationStatusRepository.GetAllByCondition(rs => rs.isStatus == status, "Reservation,Reservation.AppUser,Reservation.ReservationDetails,Reservation.ReservationDetails.AppUserEmployement.AppUser,,Reservation.ReservationDetails.AppUserEmployement.Employment");
            var listModel = new List<ReservationViewModel>();
            foreach (var reservation in reservations)
            {
                var reservationStatement = "";
                if (status == true) reservationStatement = "Onaylandı";
                if (status == false) reservationStatement = "Reddedildi";
                if(status == null)
                {
                    if(reservation.isUserEdit== false && reservation.isManagerEdit == false)
                        reservationStatement = "Beklemede - Yeni";
                    if (reservation.isUserEdit == true && reservation.isManagerEdit == false)
                        reservationStatement = "Beklemede - Kullanıcı tarafından düzenledi";
                    if (reservation.isUserEdit == false && reservation.isManagerEdit == true)
                        reservationStatement = "Beklemede - Kuaför tarafından düzenlendi";
                }

                var detailListModel = new List<ReservationDetailViewModel>();
                foreach(var detail in reservation.Reservation.ReservationDetails)
                {
                    var detailModel = new ReservationDetailViewModel(detail.AppUserEmployement.Employment.Name,detail.AppUserEmployement.AppUser.NameSurname,detail.AppUserEmployement.Duration,detail.AppUserEmployement.Price);
                    detailListModel.Add(detailModel);
                }
                var model = new ReservationViewModel {
                    ResId = reservation.ResId,
                    ResDate = reservation.Reservation.ResDate,
                    ResTime = reservation.Reservation.ResTime,
                    UserNameSurname = reservation.Reservation.AppUser.NameSurname,
                    UserPhoneNumber = reservation.Reservation.AppUser.PhoneNumber,
                    ResStatement = reservationStatement,
                    ResDetails = detailListModel,
                    TotalDuration = reservation.Reservation.TotalDuration,
                    TotalPrice = reservation.Reservation.TotalPrice,
                    ResStatus = status
                };
                listModel.Add(model);
            }
            return listModel;
        }
        public List<ReservationViewModel> GetAllByUser(string username)
        {
            var reservations = _reservationStatusRepository.GetAllByCondition(rs => rs.Reservation.AppUser.UserName==username, "Reservation,Reservation.AppUser,Reservation.ReservationDetails,Reservation.ReservationDetails.AppUserEmployement.AppUser,,Reservation.ReservationDetails.AppUserEmployement.Employment");
            var listModel = new List<ReservationViewModel>();
            foreach (var reservation in reservations)
            {
                var reservationStatement = "";
                if (reservation.isStatus == true) reservationStatement = "Onaylandı";
                if (reservation.isStatus == false) reservationStatement = "Reddedildi";
                if (reservation.isStatus == null)
                {
                    if (reservation.isUserEdit == false && reservation.isManagerEdit == false)
                        reservationStatement = "Beklemede - Yeni";
                    if (reservation.isUserEdit == true && reservation.isManagerEdit == false)
                        reservationStatement = "Beklemede - Kullanıcı tarafından düzenledi";
                    if (reservation.isUserEdit == false && reservation.isManagerEdit == true)
                        reservationStatement = "Beklemede - Kuaför tarafından düzenlendi";
                }

                var detailListModel = new List<ReservationDetailViewModel>();
                foreach (var detail in reservation.Reservation.ReservationDetails)
                {
                    var detailModel = new ReservationDetailViewModel(detail.AppUserEmployement.Employment.Name, detail.AppUserEmployement.AppUser.NameSurname, detail.AppUserEmployement.Duration, detail.AppUserEmployement.Price);
                    detailListModel.Add(detailModel);
                }
                var model = new ReservationViewModel
                {
                    ResId = reservation.ResId,
                    ResDate = reservation.Reservation.ResDate,
                    ResTime = reservation.Reservation.ResTime,
                    UserNameSurname = reservation.Reservation.AppUser.NameSurname,
                    UserPhoneNumber = reservation.Reservation.AppUser.PhoneNumber,
                    ResStatement = reservationStatement,
                    ResDetails = detailListModel,
                    TotalDuration = reservation.Reservation.TotalDuration,
                    TotalPrice = reservation.Reservation.TotalPrice,
                    ResStatus = reservation.isStatus
                };
                listModel.Add(model);
            }
            return listModel;
        }
    }
}
