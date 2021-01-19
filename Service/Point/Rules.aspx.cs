using App.Model;
using Dapper;
using MobileSoft.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service.Point
{
    public partial class Rules : System.Web.UI.Page
    {
        protected string corpId = "2015";
        protected string communityId;

        protected void Page_Load(object sender, EventArgs e)
        {
            corpId = HttpContext.Current.Request.QueryString["CorpID"];
            communityId = HttpContext.Current.Request.QueryString["CommunityID"];

            string innerHTML = "";

            if (corpId == "1509")
            {
                innerHTML = @"<h3>【积分价值】</h3>
邦客生活APP内积分价值设定为：100积分=1元。
<h3>【积分获取途径】</h3>
签到积分，每天签到可获得1积分，累计签到奖励积分,当签到累计到一定天数时，系统自动赠送签到以外的积分。
在“邦客生活”APP线上商城购物，完成线上付款后获得相应积分。
<h3>【积分使用途径】</h3>
用户在“邦客生活”APP线上商城选择参加积分兑换的商品购物付款时可选择使用积分抵扣商品价格。用户可自行选择设置积分抵扣数值。
<h3>【积分相关说明】</h3>
1.个人用户所获积分只能在个人账号下的APP内使用，积分不可转赠、转让、兑现;若购物付款使用积分抵扣价格后退货的，则积分系统将自动将该笔积分扣除，不予退还；<br/>
2.积分有效期为1年，每年12月31日将自动清零；<br/>
3.积分的获取、使用方式以及详细数据，可在APP内个人积分账户查看；<br/>
4.使用积分抵扣商品金额时，用户可自行选择是否使用积分抵扣，并可自行选择使用多少积分进行抵扣；<br/>
5.“邦客生活”APP积分规则由成都衡信英伦物业管理有限公司制定并依据相关法律法规及规章制度进行解释及修改，具体规则以“邦客生活”APP公布为准；<br/>
6.“邦客生活”APP所有积分活动的最终解释权归成都衡信英伦物业管理有限公司所有。<br/>";
                this.form1.InnerHtml = innerHTML;
                return;
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                
                // 1、物业欠费缴费抵用规则
                var deductionRules = conn.Query<Tb_App_Point_PropertyDeductionRule>(@"SELECT * FROM Tb_App_Point_PropertyDeductionRule
                                        WHERE CorpID=@CorpID AND CommunityID=@CommunityID AND IsDelete=0 AND getdate()<EndTime
                                        AND (DeductionObject=@DeductionObject1 OR DeductionObject=@DeductionObject2) 
                                        ORDER BY ConditionAmount,DiscountsAmount",
                                        new
                                        {
                                            CorpID = corpId,
                                            CommunityID = communityId,
                                            DeductionObject1 = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee),
                                            DeductionObject2 = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee)
                                        }).ToList();


                if (deductionRules.Count() > 0)
                {
                    innerHTML += "<div><span>欠费缴费积分抵用规则</span><br />";

                    for (int i = 0; i < deductionRules.Count(); i++)
                    {
                        var item = deductionRules[i];
                        innerHTML += $@"<p>{i + 1}、缴费满{item.ConditionAmount}元可用积分抵扣{item.DiscountsAmount}元，活动截止至{item.EndTime}。</p>";
                    }
                    innerHTML += "<div>";
                }

                // 2、物业欠费缴费赠送规则
                var presentedRules = conn.Query<Tb_App_Point_PropertyPresentedRule>(@"SELECT * FROM Tb_App_Point_PropertyPresentedRule
                                        WHERE CorpID=@CorpID AND CommunityID=@CommunityID AND IsDelete=0 AND getdate()<EndTime
                                        AND (PresentedObject=@PresentedObject1 OR PresentedObject=@PresentedObject2) 
                                        ORDER BY ConditionAmount,PresentedPoints",
                                        new
                                        {
                                            CorpID = corpId,
                                            CommunityID = communityId,
                                            PresentedObject1 = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee),
                                            PresentedObject2 = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee)
                                        }).ToList();



                // 2、预存缴费规则
                if (corpId == "2015") // 力帆
                {
                    innerHTML += @"<div>
                                    <span> 预存缴费赠送规则 </span>
                                    <br />
                                    <p>
                                        1、预存满6个月赠送18000
                                    </p>
                                    <p>
                                        2、预存满12个月赠送45000
                                    </p>
                                    <p>
                                        3、预存满18个月赠送63000
                                    </p>
                                    <p>
                                        4、预存满24个月赠送90000
                                    </p>
                                    <p>
                                        活动截止时间至2019年3月31号
                                    </p>

                                </div> ";
                }
                else
                {

                    if (presentedRules.Count() > 0)
                    {
                        innerHTML += "<div><span>欠费缴费积分抵扣规则</span><br />";

                        for (int i = 0; i < presentedRules.Count(); i++)
                        {
                            var item = presentedRules[i];
                            innerHTML += $@"<p>{i + 1}、缴费满{item.ConditionAmount}元赠送{item.PresentedPoints}积分，活动截止至{item.EndTime}。</p>";
                        }
                        innerHTML += "<div>";
                    }
                    else
                    {
                        innerHTML += "<div><span>暂未配置欠费缴费积分抵扣规则</span><br/>";
                    }
                }

                this.form1.InnerHtml = innerHTML;
            }
        }
    }
}