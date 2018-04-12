using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Infrastructure;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using BioEnumerator.DataAccessManager.Repository.Helpers;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using BioEnumerator.RemoteHelper.MessengerService;
using DPFP;
using Newtonsoft.Json;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Date;
using XPLUG.WINDOWTOOLS.Extensions;
using XPLUG.WINDOWTOOLS.Security;

namespace BioEnumerator.DataAccessManager.Repository
{
    internal class BeneficiaryRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<Beneficiary> _repository;
        private readonly IBioEnumeratorRepository<BeneficiaryBiometric> _beneficiaryBiometricRepo; 
        private readonly BioEnumeratorUoWork _uoWork;

        public BeneficiaryRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<Beneficiary>(_uoWork);
            _beneficiaryBiometricRepo = new BioEnumeratorRepository<BeneficiaryBiometric>(_uoWork);
        }
        

        public Status AddBeneficiary(BeneficiaryRegObj beneficiary)
        {
            
            #region Null Validation

            if (beneficiary.Equals(null))
            {
                _status.Message.FriendlyMessage = "Beneficiary Information is empty / invalid";
                _status.Message.TechnicalMessage = "Beneficiary Information is empty / invalid";
                return _status;
            }

            #endregion

            beneficiary.FinFingerTemplateData = new FingerTemplateData
            {
                FingerPrintTemplates = new List<byte[]>()
            };
            try
            {
                #region Model Validation

                // Validation

                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(beneficiary, out valResults))
                {
                    var errorDetail = new StringBuilder();
                    if (!valResults.IsNullOrEmpty())
                    {
                        errorDetail.AppendLine("Following error occurred:");
                        valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                    }
                    else
                    {
                        errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                    }
                    _status.Message.FriendlyMessage = errorDetail.ToString();
                    _status.Message.TechnicalMessage = errorDetail.ToString();
                    _status.IsSuccessful = false;
                    return _status;
                }

                #endregion


                #region Composer

                var helper = new Beneficiary
                {
                    Surname = beneficiary.Surname,
                    FirstName = beneficiary.FirstName,
                    Othernames = beneficiary.Othernames,
                    DateOfBirth = beneficiary.DateOfBirth,
                    MobileNumber = beneficiary.MobileNumber,
                    ResidentialAddress = beneficiary.ResidentialAddress,
                    OfficeAddress = beneficiary.OfficeAddress,
                    StateId = beneficiary.StateId,
                    LocalAreaId = beneficiary.LocalAreaId,
                    Sex = beneficiary.Sex,
                    OccupationId = beneficiary.OccupationId,
                    MaritalStatus = beneficiary.MaritalStatus,
                    TimeStampRegistered = DateMap.CurrentTimeStamp(),
                    Status = RegStatus.New_Registration,
                    TimeStampUploaded = null,
                    //BeneficiaryBiometric = new BeneficiaryBiometric
                    //{
                    //    _Template = JsonConvert.SerializeObject(beneficiary.FinFingerTemplateData),
                    //    ImageFileName = Path.GetFileName(imgPath),
                    //    ImagePath = imgPath
                    //}

                };

                #endregion

                using (var db = _uoWork.BeginTransaction())
                {
                    try
                    {
                        string msg;
                        var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
                        var username = Utils.CurrentUser.UserName;
                        var image = Utils.BeneficiaryRegObj.Image;

                        var imgPath = EnrollHelper.SaveImageLocally(image, stationKey, username, out msg);
                        if (imgPath.IsNullOrEmpty() || !msg.IsNullOrEmpty())
                        {
                            _status.Message.FriendlyMessage =
                                _status.Message.TechnicalMessage =
                                    (msg.IsNullOrEmpty() ? "Processing Failed! Please try again later" : msg);
                            return _status;
                        }

                        beneficiary.FinFingerTemplateData.FingerPrintTemplates = EnrollHelper.ExtractFingerTemplates();

                        var retVal = _repository.Add(helper);
                        _uoWork.SaveChanges();
                        if (retVal.BeneficiaryId < 1)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Processing Failed! Please try again later";
                            return _status;
                        }

                        #region Biometric Info

                        var biometricInfo = new BeneficiaryBiometric
                        {
                            BeneficiaryId = retVal.BeneficiaryId,
                            _Template = JsonConvert.SerializeObject(beneficiary.FinFingerTemplateData),
                            ImageFileName = Path.GetFileName(imgPath),
                            ImagePath = imgPath
                        };

                        var processBiometric = _beneficiaryBiometricRepo.Add(biometricInfo);
                        _uoWork.SaveChanges();
                        if (processBiometric.BeneficiaryBiometricId < 1)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Processing Failed! Please try again later";
                            return _status;
                        }

                        #endregion

                        db.Commit();
                        _status.ReturnedId = retVal.BeneficiaryId;
                        _status.IsSuccessful = true;
                        return _status;
                        
                    }
                    catch (DbEntityValidationException ex)
                    {
                        ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                        _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                        _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                        _status.Message.MessageCode = "101";
                        _status.Message.MessageId = 1;
                        return _status;
                    }
                    catch (Exception ex)
                    {
                        db.Rollback();
                        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                        _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                        _status.Message.TechnicalMessage = "Error: " + ex.Message;
                        _status.Message.MessageCode = "101";
                        _status.Message.MessageId = 1;
                        return _status;
                    }
                }

            }
            catch (Exception ex)
            {
                _status.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return _status;
            }
            
        }
        public Status AddBeneficiaryBiometric(BeneficiaryBiometric beneficiary)
        {

            #region Null Validation

            if (beneficiary.Equals(null))
            {
                _status.Message.FriendlyMessage = "Beneficiary Information is empty / invalid";
                _status.Message.TechnicalMessage = "Beneficiary Information is empty / invalid";
                return _status;
            }

            #endregion

            try
            {
                #region Model Validation

                // Validation

                //List<ValidationResult> valResults;
                //if (!EntityValidatorHelper.Validate(beneficiary, out valResults))
                //{
                //    var errorDetail = new StringBuilder();
                //    if (!valResults.IsNullOrEmpty())
                //    {
                //        errorDetail.AppendLine("Following error occurred:");
                //        valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                //    }
                //    else
                //    {
                //        errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                //    }
                //    _status.Message.FriendlyMessage = errorDetail.ToString();
                //    _status.Message.TechnicalMessage = errorDetail.ToString();
                //    _status.IsSuccessful = false;
                //    return _status;
                //}

                #endregion


                #region Composer

                var retId = _beneficiaryBiometricRepo.Add(beneficiary);
                _uoWork.SaveChanges();
                if (retId.BeneficiaryBiometricId < 1)
                {
                    _status.IsSuccessful = false;
                    return _status;
                }

                #endregion


                _status.ReturnedId = Convert.ToInt32(retId.BeneficiaryBiometricId);
                _status.IsSuccessful = true;
                return _status;

            }
            catch (Exception ex)
            {
                _status.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return _status;
            }

        }
        public BeneficiaryBiometric GetBeneficiaryBiometric(int id)
        {
            try
            {
                var myItem = _beneficiaryBiometricRepo.GetById(id);
                if (myItem == null || myItem.BeneficiaryBiometricId < 1)
                {
                    return new BeneficiaryBiometric();
                }

                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        public string SaveImage(int schoolSubjectId, int examYear, string title, string imageString, List<long> selectedQuestions)
        {
            try
            {

                #region Enrollment

                var uniq = DateTime.Now.Ticks;

                var bioData = new BeneficiaryBiometric
                {
                    BeneficiaryBiometricId = 1,
                    BeneficiaryId = 1,
                    ImageFileName = @uniq + "image1",
                    //IndexFileName = "indexfilename",
                    //ThumbFileName = "thumbfilename",
                    //Template = new byte[] {}
                };
                var bioStoreDir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BiometricResource"];


                #endregion

                var i = 1;
                var fileName = "Image " + i + ".gif";
                //TODO:Resourse path
                //var folderPath = "PostJamb/" + CustomHelper.RemoveSpecialCharacters(GetSchool(schoolSubjectId)) + "/" + CustomHelper.RemoveSpecialCharacters(GetSubject(schoolSubjectId)) + "/" + examYear + "/Image/";
                var folderPath = "";
                var imageResPath = folderPath + fileName;
                var dir = System.Configuration.ConfigurationManager.AppSettings["ExamResource"];
                var imagePath = System.IO.Path.GetFullPath(dir + imageResPath);
                if (!Directory.Exists(@dir + @folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(@dir + @folderPath);
                    }
                    catch (Exception)
                    {
                        return null;
                        //return Json(new { success = false, error = "An error occured while creating upload directory. Please try again later" }, JsonRequestBehavior.AllowGet);
                    }
                }
                while (System.IO.File.Exists(imagePath))
                {
                    i++;
                    fileName = "Image " + i + ".gif";
                    imageResPath = folderPath + fileName;
                    imagePath = Path.GetFullPath(dir + imageResPath);
                }
                byte[] bytes = Convert.FromBase64String(imageString);
                System.IO.File.WriteAllBytes(@imagePath, bytes);

                //var contract = new PQuestionImageContract
                //{
                //    Title = title,
                //    ImagePath = "~/" + imageResPath,
                //    Status = Convert.ToInt32(StatusEnum.Insert),
                //    ExamYear = examYear,
                //    SchoolSubjectId = schoolSubjectId,
                //};
                //var imageId = ServiceProvider.Instance().GetPQuestionImageServices().CustomAddQuestionImage(contract, selectedQuestions);
                //if (imageId < 1)
                //{
                //    return null;
                //    //return Json(new { success = false, error = "An unexpected error occured! Please try again later" }, JsonRequestBehavior.AllowGet);
                //}
                return null;
                //return Json(new { success = true, imageId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
                //return Json(new { success = false, error = "An unexpected error occured! Please try again later" }, JsonRequestBehavior.AllowGet);
            }
        }

        #region CRUD

        public bool UpdateBeneficiary(Beneficiary beneficiary, out string msg)
        {
            try
            {

                #region Model Validation

                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(beneficiary, out valResults))
                {
                    var errorDetail = new StringBuilder();
                    if (!valResults.IsNullOrEmpty())
                    {
                        errorDetail.AppendLine("Following error occurred:");
                        valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                    }
                    else
                    {
                        errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                    }

                    msg = errorDetail.ToString();
                    return false;
                }

                #endregion

                if (beneficiary.BeneficiaryId < 1)
                {
                    msg = "Invalid Record Id! Unable to complete operation on this record";
                    return false;
                }

                var processedBeneficiary = _repository.Update(beneficiary);
                _uoWork.SaveChanges();
                if (processedBeneficiary.BeneficiaryId < 1)
                {
                    msg = "Processing Failed! Please try again later";
                    return false;
                }

                msg = "";
                return processedBeneficiary.BeneficiaryId > 0;

            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }
        public BeneficiaryObj GetBeneficiaryObj(long id)
        {
            
            try
            {
                var myItem = _repository.GetById(id);
                if (myItem == null || myItem.BeneficiaryId < 1)
                {
                    return new BeneficiaryObj();
                }

                var info = _beneficiaryBiometricRepo.GetById(myItem.BeneficiaryId);
                if (info == null || info.BeneficiaryBiometricId < 1)
                {
                    return new BeneficiaryObj();
                }

                var fingerTemplates = GetFingerTemplates(info.FingerTemplate.FingerPrintTemplates);
                if (fingerTemplates == null || Array.TrueForAll(fingerTemplates, x => x.Equals(null)))
                {
                    return new BeneficiaryObj();
                }

                //var imgByte = CustomHelper.ConvertImageToByte(info.ImagePath);
                var imgByteString = CustomHelper.ConvertImageToBase64String(info.ImagePath);

                var retItem = new BeneficiaryObj
                {
                    BeneficiaryId = myItem.BeneficiaryId,
                    BeneficiaryBiometricId = info.BeneficiaryBiometricId,
                    Surname = myItem.Surname,
                    Firstname = myItem.FirstName,
                    Othernames = myItem.Othernames,
                    DateOfBirth = myItem.DateOfBirth,
                    MobileNumber = myItem.MobileNumber,
                    ResidentialAddress = myItem.ResidentialAddress,
                    OfficeAddress = myItem.OfficeAddress,
                    OccupationId = myItem.OccupationId,
                    MaritalStatus = myItem.MaritalStatus,
                    Sex = myItem.Sex,
                    Status = (int)myItem.Status,
                    UploadStatus = (int)myItem.UploadStatus,
                    //TimeStampUploaded = myItem.TimeStampUploaded.GetValueOrDefault(),
                    TimestampRegistered = myItem.TimeStampRegistered,
                    ImageFileName = info.ImageFileName,
                    ImagePath = info.ImagePath,
                    //Image = Image.FromFile(info.ImagePath),
                    //ImageByteArray = imgByte,
                    ImageByteString = imgByteString,
                    FingerPrintTemplates = new List<byte[]>(info.FingerTemplate.FingerPrintTemplates),
                    FingerTemplates = fingerTemplates
                };

                return retItem;

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        public Beneficiary GetBeneficiary(long id)
        {

            try
            {
                var myItem = _repository.GetById(id);
                if (myItem == null || myItem.BeneficiaryId < 1)
                {
                    return new Beneficiary();
                }

                //var info = _beneficiaryBiometricRepo.GetById(myItem.BeneficiaryId);
                //if (info == null || info.BeneficiaryBiometricId < 1)
                //{
                //    return new BeneficiaryObj();
                //}
                
                return myItem;

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        private Beneficiary GetBeneficiaryInfoRaw(long beneficiaryId)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(string.Format("SELECT * FROM Beneficiary WHERE BeneficiaryId = {0};", beneficiaryId));
                var retVal = _repository.RepositoryContext()
                     .Database.SqlQuery<Beneficiary>(sql.ToString()).ToList();
                if (retVal.IsNullOrEmpty()) { return null; }
                return retVal[0];
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public List<BeneficiaryObj> GetBeneficiarys()
        {

            try
            {
                var all = _repository.GetAll().ToList();
                if (!all.Any())
                {
                    return new List<BeneficiaryObj>();
                }
                
                var retItems = new List<BeneficiaryObj>();
                all.ForEachx(m =>
                {
                    var thisBioMetric = (new BeneficiaryBiometricRepository().GetBeneficiaryBiometric(m.BeneficiaryId) ??
                                    new BeneficiaryBiometric());
                    var fingerTemplates = GetFingerTemplates(thisBioMetric.FingerTemplate.FingerPrintTemplates);
                    var state = (new StateRepository().GetState(m.StateId) ??
                                    new State());
                    var lga = (new LocalAreaRepository().GetLocalArea(m.LocalAreaId) ??
                                    new LocalArea());

                    retItems.Add(new BeneficiaryObj
                    {
                        BeneficiaryId = m.BeneficiaryId,
                        BeneficiaryBiometricId = thisBioMetric.BeneficiaryBiometricId,
                        Surname = m.Surname,
                        Firstname = m.FirstName,
                        Othernames = m.Othernames,
                        DateOfBirth = m.DateOfBirth,
                        MobileNumber = m.MobileNumber,
                        ResidentialAddress = m.ResidentialAddress,
                        OfficeAddress = m.OfficeAddress,
                        OccupationId = m.OccupationId,
                        MaritalStatus = m.MaritalStatus,
                        StateId = m.StateId,
                        StateLabel = state.Name,
                        LocalAreaId = m.LocalAreaId,
                        LocalAreaLabel = lga.Name,
                        Sex = m.Sex,
                        Status = (int)m.Status,
                        UploadStatus = (int)m.UploadStatus,
                        TimeStampUploaded = m.TimeStampUploaded,
                        TimestampRegistered = m.TimeStampRegistered,
                        ImageFileName = thisBioMetric.ImageFileName,
                        ImagePath = thisBioMetric.ImagePath,
                        FingerPrintTemplates = new List<byte[]>(thisBioMetric.FingerTemplate.FingerPrintTemplates),
                        FingerTemplates = fingerTemplates
                    });
                    
                });
                
                return retItems;

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public List<BeneficiaryObj> GetBeneficiaryReportDetail(string surname, string mobileNo)
        {
            try
            {
                var retItems = new List<BeneficiaryObj>();
                //localAreaId = 137;

                var sql = new StringBuilder();
                //sql.Append("SELECT *, 0 as IsSelected, '' as StatusLabel, '' as UploadStatusLabel, '' as SexLabel FROM Beneficiary WHERE 1 = 1");
                //sql.Append("SELECT * FROM Beneficiary A WHERE 1 = 1");
                sql.Append("SELECT * FROM Beneficiary WHERE 1 = 1");

                //SELECT * FROM a WHERE q_date = '2012-06-04' AND q_time = '05:06:00';
                //sql.Append(string.Format(" AND substr(A.TimestampRegistered, 0, 10) >= '{0}' AND substr(A.TimestampRegistered, 0, 10) <= '{1}'", startDate.Trim(), stopDate.Trim()));
                //sql.Append(string.Format(" AND A.TimestampRegistered >= '{0}' AND A.TimestampRegistered <= '{1}'", startDate, stopDate));
                //DateTime.ParseExact(dateString, "yyyy/MM/dd");
                //sql.Append(string.Format(" AND DateTime.ParseExact(A.TimestampRegistered, 'yyyy/MM/dd') >= '{0}' AND DateTime.ParseExact(A.TimestampRegistered, 'yyyy/MM/dd') <= '{1}'", startDate, stopDate));

              
                if (!string.IsNullOrEmpty(surname))
                {
                    sql.Append(string.Format(" AND Surname Like '%{0}%'", surname.Trim()));
                }
                if (!string.IsNullOrEmpty(mobileNo))
                {
                    sql.Append(string.Format(" AND MobileNumber Like '%{0}%'", mobileNo.Trim()));
                }

                var retVal = _repository.RepositoryContext()
                      .Database.SqlQuery<Beneficiary>(sql.ToString()).ToList();
                if (retVal.IsNullOrEmpty()) { return new List<BeneficiaryObj>(); }

                retVal.ForEachx(m =>
                {
                    var thisBioMetric = (new BeneficiaryBiometricRepository().GetBeneficiaryBiometric(m.BeneficiaryId) ??
                                    new BeneficiaryBiometric());
                    var fingerTemplates = GetFingerTemplates(thisBioMetric.FingerTemplate.FingerPrintTemplates);
                    var state = (new StateRepository().GetState(m.StateId) ??
                                    new State());
                    var lga = (new LocalAreaRepository().GetLocalArea(m.LocalAreaId) ??
                                    new LocalArea());

                    retItems.Add(new BeneficiaryObj
                    {
                        BeneficiaryId = m.BeneficiaryId,
                        //BeneficiaryBiometricId = thisBioMetric.BeneficiaryBiometricId,
                        Surname = m.Surname,
                        Firstname = m.FirstName,
                        Othernames = m.Othernames,
                        DateOfBirth = m.DateOfBirth,
                        MobileNumber = m.MobileNumber,
                        ResidentialAddress = m.ResidentialAddress,
                        OfficeAddress = m.OfficeAddress,
                        OccupationId = m.OccupationId,
                        MaritalStatus = m.MaritalStatus,
                        StateId = m.StateId,
                        StateLabel = state.Name,
                        LocalAreaId = m.LocalAreaId,
                        LocalAreaLabel = lga.Name,
                        Sex = m.Sex,
                        Status = (int)m.Status,
                        UploadStatus = (int)m.UploadStatus,
                        TimeStampUploaded = m.TimeStampUploaded,
                        TimestampRegistered = m.TimeStampRegistered,
                        ImageFileName = thisBioMetric.ImageFileName,
                        ImagePath = thisBioMetric.ImagePath,
                        FingerPrintTemplates = new List<byte[]>(thisBioMetric.FingerTemplate.FingerPrintTemplates),
                        FingerTemplates = fingerTemplates
                    });
                });

                return retItems;
                //return retVal;
            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }
        public List<BeneficiaryObj> GetBeneficiaryReportDetail(string startDate, string stopDate, string surname, string mobileNo, int sexId, int stateId, int localAreaId)
        {
            try
            {
                var retItems = new List<BeneficiaryObj>();
                //localAreaId = 137;

                var sql = new StringBuilder();
                //sql.Append("SELECT *, 0 as IsSelected, '' as StatusLabel, '' as UploadStatusLabel, '' as SexLabel FROM Beneficiary WHERE 1 = 1");
                //sql.Append("SELECT * FROM Beneficiary A WHERE 1 = 1");
                sql.Append("SELECT * FROM Beneficiary WHERE 1 = 1");

                //SELECT * FROM a WHERE q_date = '2012-06-04' AND q_time = '05:06:00';
                //sql.Append(string.Format(" AND substr(A.TimestampRegistered, 0, 10) >= '{0}' AND substr(A.TimestampRegistered, 0, 10) <= '{1}'", startDate.Trim(), stopDate.Trim()));
                //sql.Append(string.Format(" AND A.TimestampRegistered >= '{0}' AND A.TimestampRegistered <= '{1}'", startDate, stopDate));
                //DateTime.ParseExact(dateString, "yyyy/MM/dd");
                //sql.Append(string.Format(" AND DateTime.ParseExact(A.TimestampRegistered, 'yyyy/MM/dd') >= '{0}' AND DateTime.ParseExact(A.TimestampRegistered, 'yyyy/MM/dd') <= '{1}'", startDate, stopDate));

                //sql.Append(string.Format(" AND SUBSTR(TimeStampRegistered, 0, 10) >= '{0}' AND SUBSTR(TimeStampRegistered, 0, 10) <= '{1}'", startDate.Trim(), stopDate.Trim()));
                if (sexId > 0)
                {
                    sql.Append(string.Format(" AND Sex  = {0}", sexId));
                }
                if (!string.IsNullOrEmpty(surname))
                {
                    sql.Append(string.Format(" AND Surname Like '%{0}%'", surname.Trim()));
                }
                if (!string.IsNullOrEmpty(mobileNo))
                {
                    sql.Append(string.Format(" AND MobileNumber Like '%{0}%'", mobileNo.Trim()));
                }
                if (stateId > 0)
                {
                    sql.Append(string.Format(" AND StateId = {0}", stateId));
                }
                if (localAreaId > 0)
                {
                    sql.Append(string.Format(" AND LocalAreaId = {0}", localAreaId));
                }
               
                var retVal = _repository.RepositoryContext()
                      .Database.SqlQuery<Beneficiary>(sql.ToString()).ToList();
                if (retVal.IsNullOrEmpty()) { return new List<BeneficiaryObj>(); }
                
                retVal.ForEachx(m =>
                {
                    var thisBioMetric = (new BeneficiaryBiometricRepository().GetBeneficiaryBiometric(m.BeneficiaryId) ??
                                    new BeneficiaryBiometric());
                    var fingerTemplates = GetFingerTemplates(thisBioMetric.FingerTemplate.FingerPrintTemplates);
                    var state = (new StateRepository().GetState(m.StateId) ??
                                    new State());
                    var lga = (new LocalAreaRepository().GetLocalArea(m.LocalAreaId) ??
                                    new LocalArea());

                    retItems.Add(new BeneficiaryObj
                    {
                        BeneficiaryId = m.BeneficiaryId,
                        //BeneficiaryBiometricId = thisBioMetric.BeneficiaryBiometricId,
                        Surname = m.Surname,
                        Firstname = m.FirstName,
                        Othernames = m.Othernames,
                        DateOfBirth = m.DateOfBirth,
                        MobileNumber = m.MobileNumber,
                        ResidentialAddress = m.ResidentialAddress,
                        OfficeAddress = m.OfficeAddress,
                        OccupationId = m.OccupationId,
                        MaritalStatus = m.MaritalStatus,
                        StateId = m.StateId,
                        StateLabel = state.Name,
                        LocalAreaId = m.LocalAreaId,
                        LocalAreaLabel = lga.Name,
                        Sex = m.Sex,
                        Status = (int)m.Status,
                        UploadStatus = (int)m.UploadStatus,
                        TimeStampUploaded = m.TimeStampUploaded,
                        TimestampRegistered = m.TimeStampRegistered,
                        ImageFileName = thisBioMetric.ImageFileName,
                        ImagePath = thisBioMetric.ImagePath,
                        FingerPrintTemplates = new List<byte[]>(thisBioMetric.FingerTemplate.FingerPrintTemplates),
                        FingerTemplates = fingerTemplates
                    });
                });

                return retItems;
                //return retVal;
            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }
        public List<BeneficiaryObj> GetBeneficiaryReportDetail(string surname, string mobileNo, int stateId, int localAreaId, int status)
        {
            try
            {
                var retItems = new List<BeneficiaryObj>();
                //localAreaId = 137;

                var sql = new StringBuilder();
                //sql.Append("SELECT *, 0 as IsSelected, '' as StatusLabel, '' as UploadStatusLabel, '' as SexLabel FROM Beneficiary WHERE 1 = 1");
                //sql.Append("SELECT * FROM Beneficiary A WHERE 1 = 1");
                sql.Append("SELECT * FROM Beneficiary WHERE 1 = 1");

                //SELECT * FROM a WHERE q_date = '2012-06-04' AND q_time = '05:06:00';
                //sql.Append(string.Format(" AND substr(A.TimestampRegistered, 0, 10) >= '{0}' AND substr(A.TimestampRegistered, 0, 10) <= '{1}'", startDate.Trim(), stopDate.Trim()));
                //sql.Append(string.Format(" AND A.TimestampRegistered >= '{0}' AND A.TimestampRegistered <= '{1}'", startDate, stopDate));
                //DateTime.ParseExact(dateString, "yyyy/MM/dd");
                //sql.Append(string.Format(" AND DateTime.ParseExact(A.TimestampRegistered, 'yyyy/MM/dd') >= '{0}' AND DateTime.ParseExact(A.TimestampRegistered, 'yyyy/MM/dd') <= '{1}'", startDate, stopDate));

                //sql.Append(string.Format(" AND substr(TimestampRegistered, 0, 10) >= '{0}' AND substr(TimestampRegistered, 0, 10) <= '{1}'", startDate.Trim(), stopDate.Trim()));
               
                if (!string.IsNullOrEmpty(surname))
                {
                    sql.Append(string.Format(" AND Surname Like '%{0}%'", surname.Trim()));
                }
                if (!string.IsNullOrEmpty(mobileNo))
                {
                    sql.Append(string.Format(" AND MobileNumber Like '%{0}%'", mobileNo.Trim()));
                }
                if (stateId > 0)
                {
                    sql.Append(string.Format(" AND StateId = {0}", stateId));
                }
                if (localAreaId > 0)
                {
                    sql.Append(string.Format(" AND LocalAreaId = {0}", localAreaId));
                }
                //if (status > 0)
                //{
                //    sql.Append(string.Format(" AND Sex  = {0}", sexId));
                //}
                if (status > -2 && status != 0)
                {
                    sql.Append(status == -1 ? " AND UploadStatus = -1" : string.Format(" AND Status = {0}", status));
                }
                else
                {
                    sql.Append(" AND Status <> 2");
                }


                var retVal = _repository.RepositoryContext()
                      .Database.SqlQuery<Beneficiary>(sql.ToString()).ToList();
                if (retVal.IsNullOrEmpty()) { return new List<BeneficiaryObj>(); }

                retVal.ForEachx(m =>
                {
                    var thisBioMetric = (new BeneficiaryBiometricRepository().GetBeneficiaryBiometric(m.BeneficiaryId) ??
                                    new BeneficiaryBiometric());
                    var fingerTemplates = GetFingerTemplates(thisBioMetric.FingerTemplate.FingerPrintTemplates);
                    var state = (new StateRepository().GetState(m.StateId) ??
                                    new State());
                    var lga = (new LocalAreaRepository().GetLocalArea(m.LocalAreaId) ??
                                    new LocalArea());
                    var imgByte = CustomHelper.ConvertImageToByte(thisBioMetric.ImagePath);
                    var imgByteString = CustomHelper.ConvertImageToBase64String(thisBioMetric.ImagePath);

                    retItems.Add(new BeneficiaryObj
                    {
                        BeneficiaryId = m.BeneficiaryId,
                        //BeneficiaryBiometricId = thisBioMetric.BeneficiaryBiometricId,
                        Surname = m.Surname,
                        Firstname = m.FirstName,
                        Othernames = m.Othernames,
                        DateOfBirth = m.DateOfBirth,
                        MobileNumber = m.MobileNumber,
                        ResidentialAddress = m.ResidentialAddress,
                        OfficeAddress = m.OfficeAddress,
                        OccupationId = m.OccupationId,
                        MaritalStatus = m.MaritalStatus,
                        StateId = m.StateId,
                        StateLabel = state.Name,
                        LocalAreaId = m.LocalAreaId,
                        LocalAreaLabel = lga.Name,
                        Sex = m.Sex,
                        Status = (int)m.Status,
                        UploadStatus = (int)m.UploadStatus,
                        TimeStampUploaded = m.TimeStampUploaded,
                        TimestampRegistered = m.TimeStampRegistered,
                        ImageFileName = thisBioMetric.ImageFileName,
                        ImagePath = thisBioMetric.ImagePath,
                        Image = Image.FromFile(thisBioMetric.ImagePath),
                        ImageByteArray = imgByte,
                        ImageByteString = imgByteString,
                        //BitmapImage = Image.FromFile(thisBioMetric.ImagePath + thisBioMetric.ImageFileName),
                        FingerPrintTemplates = new List<byte[]>(thisBioMetric.FingerTemplate.FingerPrintTemplates),
                        FingerTemplates = fingerTemplates
                    });
                });

                return retItems;
                //return retVal;
            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<BeneficiaryObj>();
            }
        }

        public DashboardDataCount GetDashboardDataCount()
        {
            try
            {
                var myObj = new DashboardDataCount();
                var sql = "SELECT coalesce(Count(BeneficiaryId), 0) as Count FROM Beneficiary";
                var sql2 = "SELECT coalesce(Count(BeneficiaryId), 0) as Count FROM Beneficiary WHERE UploadStatus = 1";
                var sql3 = "SELECT coalesce(Count(BeneficiaryId), 0) as Count FROM Beneficiary WHERE UploadStatus = 0";
                var sql4 = "SELECT coalesce(Count(BeneficiaryId), 0) as Count FROM Beneficiary WHERE UploadStatus = -1";


                var allData = _repository.RepositoryContext()
                      .Database.SqlQuery<int>(sql).ToList();
                if (allData.Any())
                {
                    myObj.AllData = allData[0];
                }

                var uploadedData = _repository.RepositoryContext()
                      .Database.SqlQuery<int>(sql2).ToList();

                if (uploadedData.Any())
                {
                    myObj.Uploaded = uploadedData[0];
                }

                var pendingData = _repository.RepositoryContext()
                      .Database.SqlQuery<int>(sql3).ToList();

                if (pendingData.Any())
                {
                    myObj.Pending = pendingData[0];
                }


                var errorgData = _repository.RepositoryContext()
                      .Database.SqlQuery<int>(sql4).ToList();

                if (errorgData.Any())
                {
                    myObj.Error = errorgData[0];
                }

                return myObj;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new DashboardDataCount(); ;
            }
        }

        

        #endregion


        #region Upload Data
        public BulkBeneficiaryRegResponseObj UploadBulkData(BulkBeneficiaryRegObj bulkItems, UploadStationInfo stationInfo)
        {
            var response = new BulkBeneficiaryRegResponseObj
            {
                MainStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage()
                },
                BeneficiaryRegResponses = new List<BeneficiaryRegResponseObj>()
            };

            try
            {
                string msg;
                var retObj = new RemoteMessanger(RemoteProcessType.BulkBeneficiaryRegistration, stationInfo).ProcessBulkData(bulkItems, out msg);
                if (retObj == null)
                {
                    response.MainStatus.Message.FriendlyMessage =
                        response.MainStatus.Message.TechnicalMessage =
                            string.IsNullOrEmpty(msg) ? "Upload Failed! Please try again later" : msg;
                    return response;
                }

                if (!retObj.MainStatus.IsSuccessful)
                {
                    return response;
                }

                if (retObj.BeneficiaryRegResponses.IsNullOrEmpty())
                {
                    response.MainStatus.Message.FriendlyMessage =
                       response.MainStatus.Message.TechnicalMessage =
                             "Returned Collection is empty / invalid";
                    return response;
                }


                #region Manipulation

                foreach (var item in retObj.BeneficiaryRegResponses)
                {
                    try
                    {
                        var thisBenInfo = GetBeneficiaryInfoRaw(item.BeneficiaryId);
                        if (thisBenInfo == null || thisBenInfo.BeneficiaryId < 1)
                        {
                            item.Status.Message.FriendlyMessage = "No local record found";
                            response.BeneficiaryRegResponses.Add(item);
                            continue;
                        }
                        if (item.Status.IsSuccessful)
                        {
                            thisBenInfo.BeneficiaryRemoteId = item.BeneficiaryRemoteId;
                            thisBenInfo.UploadStatus = UploadStatus.Successful;
                            thisBenInfo.TimeStampUploaded = DateMap.CurrentTimeStamp();
                            thisBenInfo.UploadMessage = "";
                            thisBenInfo.Status = RegStatus.Uploaded;
                        }
                        else
                        {
                            thisBenInfo.UploadStatus = UploadStatus.Failed;
                            thisBenInfo.UploadMessage = string.IsNullOrEmpty(item.Status.Message.FriendlyMessage)
                                ? "Unknown Error"
                                : item.Status.Message.FriendlyMessage;
                            thisBenInfo.TimeStampUploaded = DateMap.CurrentTimeStamp();
                        }

                        var processedBusinessInfo = _repository.Update(thisBenInfo);
                        _uoWork.SaveChanges();
                        if (processedBusinessInfo.BeneficiaryId < 1)
                        {
                            item.Status.Message.FriendlyMessage = "Unable to update local record";
                        }
                        response.BeneficiaryRegResponses.Add(item);
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.GetBaseException().Message);
                        item.Status.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                        item.Status.Message.TechnicalMessage = "Error: " + ex.Message;
                        response.BeneficiaryRegResponses.Add(item);
                    }
                }

                #endregion
                
                response.MainStatus.IsSuccessful = true;
                return response;

            }
            catch (Exception ex)
            {
                response.MainStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                response.MainStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.GetBaseException().Message);
                return response;
            }
        }

        #endregion























        #region --> Biometric
        public List<byte[]> GetTemplates()
        {
            try
            {
                var all = _beneficiaryBiometricRepo.GetAll().ToList();
                return !all.Any() ? new List<byte[]>() : all[0].FingerTemplate.FingerPrintTemplates;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        public Template[] GetFingerTemplates()
        {

            try
            {
                var all = _beneficiaryBiometricRepo.GetAll().ToList();
                if (!all.Any())
                {
                    return null;
                }
                
                var fingerBytes = all[0].FingerTemplate.FingerPrintTemplates;
                var retTemplate = new Template[fingerBytes.Count];
                var i = 0;
                foreach (var fingerByte in fingerBytes)
                {
                    var dataStream = new MemoryStream(fingerByte.ToArray());
                    var actualTemplate = new Template();
                    actualTemplate.DeSerialize(dataStream);
                    retTemplate[i] = actualTemplate;

                    i++;
                }

                return retTemplate;
                //return !all.Any() ? new List<byte[]>() : all[0].FingerTemplate.FingerPrintTemplates;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        public Template[] GetAllFingerTemplates()
        {

            try
            {
                var all = _beneficiaryBiometricRepo.GetAll().ToList();
                if (!all.Any())
                {
                    return null;
                }

                var fingerBytes = all[0].FingerTemplate.FingerPrintTemplates;
                var retTemplate = new Template[fingerBytes.Count];
                var i = 0;
                foreach (var fingerByte in fingerBytes)
                {
                    var dataStream = new MemoryStream(fingerByte.ToArray());
                    var actualTemplate = new Template();
                    actualTemplate.DeSerialize(dataStream);
                    retTemplate[i] = actualTemplate;

                    i++;
                }

                return retTemplate;
                //return !all.Any() ? new List<byte[]>() : all[0].FingerTemplate.FingerPrintTemplates;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        public Template[] GetFingerTemplates(List<byte[]> fingerByteLists)
        {

            try
            {
                
                if (!fingerByteLists.Any())
                {
                    return null;
                }

                var retTemplate = new Template[fingerByteLists.Count];
                var i = 0;
                foreach (var fingerByte in fingerByteLists)
                {
                    var dataStream = new MemoryStream(fingerByte.ToArray());
                    var actualTemplate = new Template();
                    actualTemplate.DeSerialize(dataStream);
                    retTemplate[i] = actualTemplate;

                    i++;
                }

                return retTemplate;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 

        #endregion


    }
}
