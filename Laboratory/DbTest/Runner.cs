using Laboratory.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Laboratory.DbTest
{
    /// <summary>
    /// 数据库测试
    /// </summary>
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "Db Test(SQL2016、SQLite)";
            }
        }

        public void Run()
        {
            using (var db = new SQLContext())
            {

                EFExtend.Instance.Merge<Album>(db, db.Albums.First());

                //Console.WriteLine(GetColumnName(typeof(Album), nameof(Album.GenreId), db));
            }

            //this.SQLiteTest();
            return;

            EFExtend.Instance.Trans((command, trans) =>
            {
                command.CommandText = "SELECT TOP 100 [Id], [UserGuid], [RealName] FROM [SystemUsers]";
                var blocks = command.ExecuteReader();
                while (blocks.Read())
                {
                    Console.WriteLine($"Id: {blocks[0]}, UserGuid:{blocks[1]}, RealName:{blocks[2]}.");
                }
                trans.Commit();
            });

            return;
            this.MixedDbTest();
            return;

            using (var connection = new SqlConnection(AppConfig.MSSQL2016_CONNECTION_STRING))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 ArtistId FROM Artists";
                    var artistId = cmd.ExecuteScalar();

                    Console.WriteLine(" ExecuteScalar Return: {0}", artistId);
                }
            }

            this.SQLAETest();
            this.SQLiteTest();
        }

        /// <summary>
        /// SQLAE Test
        /// </summary>
        private void SQLAETest()
        {
            #region ADO.NET Reader

            Console.WriteLine("ADO.NET Reader Executing...");

            using (var connection = new SqlConnection(AppConfig.MSSQL2016_CONNECTION_STRING))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT ArtistId, [Name] FROM Artists";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var builder = new StringBuilder("ArtistId       Name");
                        while (reader.Read())
                        {
                            builder.AppendLine();
                            builder.AppendFormat("{0}     {1}", reader["ArtistId"], reader["Name"]);
                        }
                        Console.WriteLine(builder.ToString());
                    }
                }
            }

            Console.WriteLine("ADO.NET Reader Executed");

            #endregion

            #region EntityFramework

            Console.WriteLine("EntityFramework Executing...");

            using (var context = new SQLContext())
            {
                context.Artists.Add(new Artist
                {
                    Name = "Aaron Copland & London Symphony Orchestra"
                });

                context.SaveChanges();
            }
            Console.WriteLine("EntityFramework Executed");

            #endregion

            #region ADO.NET

            Console.WriteLine("ADO.NET Executing...");

            using (var connection = new SqlConnection(AppConfig.MSSQL2016_CONNECTION_STRING))
            {
                var transName = "trans_sql_ae";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var transaction = connection.BeginTransaction(transName))
                {
                    try
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "dbo.Insert_AE";
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                ParameterName = "@Name",
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 50,
                                Value = "Aaron Goldberg",
                                Direction = ParameterDirection.Input
                            });
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback(transName);
                        throw;
                    }
                }
            }

            Console.WriteLine("ADO.NET Executed");

            #endregion
        }

        /// <summary>
        /// SQLite Test
        /// </summary>
        private void SQLiteTest()
        {
            using (var context = new SQLiteContext())
            {
                if (context.Artists != null && context.Artists.Any())
                {
                    Console.WriteLine("ArtistId     Name");
                    foreach (var artist in context.Artists)
                    {
                        Console.WriteLine("{0}      {1}", artist.ArtistId, artist.Name);
                    }
                }
            }
        }

        /// <summary>
        /// 多种数据库混合模式
        /// </summary>
        private void MixedDbTest()
        {
            try
            {
                var companies = new List<int>();

                var logs = new StringBuilder();

                var debug_prefix = "test_";

#if !DEBUG
                debug_prefix = string.Empty;
#endif

                #region SQLServer

                using (var connection = new SqlConnection(AppConfig.MSSQL_CONNECTION_STRING))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT [CompanyNo] FROM [Companies]";

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            companies.Add(Convert.ToInt32(reader[0]));
                        }
                    }
                }

                logs.Append("搜索到的圈子：");
                logs.AppendLine(string.Join(",", companies));
                logs.AppendLine();

                #endregion

                #region MongoDb
                {
                    //控件前缀
                    var control_id_prefix = "cid";
                    //去除空格
                    var regex_tw = new Regex(@"\s+");
                    //匹配数字
                    var regex_digit = new Regex(@"\d+");
                    //id在右侧
                    var regex_id_ir = new Regex(@"<=\S+", RegexOptions.IgnorePatternWhitespace);
                    //id在左侧
                    var regex_id_il = new Regex(@"[<|=]\S+", RegexOptions.IgnorePatternWhitespace);
                    //替换formjson
                    var regex_json_id = new Regex("\\\"id\\\":\\d+,", RegexOptions.IgnorePatternWhitespace);

                    var mongoClient = new MongoClient(AppConfig.MONGO_CONNECTION_STRING);

                    #region 更新已完结的项目审批状态
                    {
                        foreach (var company in companies)
                        {
                            var mongoDb = mongoClient.GetDatabase(String.Concat(debug_prefix, "company_", company));
                            logs.AppendLine(String.Format("圈子:{0}", company));

                            var workflowRecords = mongoDb.GetCollection<FormWorkflowRecord>("FormWorkflowRecord");
                            var formRecords = mongoDb.GetCollection<FormRecord>("FormRecord");

                            var finished_status = new List<EnumFormStatus>() {
                                EnumFormStatus.Pass,
                                EnumFormStatus.Refuse,
                                EnumFormStatus.Cancel
                            };

                            var finished_records = formRecords.Find(w => finished_status.Contains(w.Status))
                                .ToList();

                            foreach (var finished_record in finished_records)
                            {
                                var finished_workflow_records = workflowRecords.Find(w => w.FormRecordGuid == finished_record.FormRecordGuid)
                                    .SortByDescending(o => o.UpdateOnUtc)
                                    .ThenByDescending(o => o.SortIndex)
                                    .ToList();

                                if (finished_workflow_records.Count > 0)
                                {
                                    var finished_workflow_record = finished_workflow_records.First();

                                    logs.AppendLine(String.Format("记录 FormRecord.FormRecordGuid {0}", finished_record.FormRecordGuid));

                                    if (finished_workflow_record.UpdateOnUtc > 0)
                                    {
                                        finished_record.FinishedOnUtc = finished_workflow_record.UpdateOnUtc;
                                    }

                                    formRecords.FindOneAndReplace(Builders<FormRecord>.Filter.Eq("_id", finished_record._id), finished_record);
                                }
                                else
                                {
                                    logs.AppendLine(String.Format("FormRecord.FormRecordGuid {0}不存在审批记录", finished_record.FormRecordGuid));
                                }
                            }
                        }
                        Logger.Instance.Log(logs.ToString());
                        return;
                    }
                    #endregion

                    #region 2018/02/24/由于上次更新模板中控件编号全部为数字，导致在对比值时可能发生错误，此次为修复该问题。
                    {
                        foreach (var company in companies)
                        {
                            var mongoDb = mongoClient.GetDatabase(String.Concat(debug_prefix, "company_", company));
                            logs.AppendLine(String.Format("圈子:{0}", company));

                            var tpls = mongoDb.GetCollection<FormTemplate>("FormTemplate");

                            var condition_tpls = tpls.Find(w => !string.IsNullOrEmpty(w.ConditionValues))
                                .ToList();

                            var workflows = mongoDb.GetCollection<ApprovalWorkflow>("ApprovalWorkflow");

                            int digit_id;
                            foreach (var condition_tpl in condition_tpls)
                            {
                                logs.AppendLine();
                                if (!int.TryParse(condition_tpl.ConditionName, out digit_id)) continue;

                                #region 模板及流程信息
                                {
                                    var tpl_new_values = new List<string>();
                                    var id = string.Concat(control_id_prefix, condition_tpl.ConditionName);

                                    logs.AppendLine(string.Format("origin values: {0} ", condition_tpl.ConditionValues));

                                    #region 更新条件值(FormTemplate)
                                    {
                                        var values = condition_tpl.ConditionValues.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                        if (values.Length == 2) //特殊情况，数据源格式有误，单独处理
                                        {
                                            var start_oi = condition_tpl.ConditionValues.IndexOf("<");
                                            var last_oi = condition_tpl.ConditionValues.LastIndexOf("<");

                                            if (last_oi > -1)
                                            {
                                                var new_condition_value = string.Concat(id, condition_tpl.ConditionValues.Substring(start_oi, last_oi - start_oi), "<=", id);
                                                var new_condition_values = new_condition_value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                                for (int i = 0, j = values.Length; i < j; i++)
                                                {
                                                    var value = values[i];
                                                    var filters = new List<FilterDefinition<ApprovalWorkflow>>() {
                                                        Builders<ApprovalWorkflow>.Filter.Eq("FormGuid", condition_tpl.FormGuid),
                                                        Builders<ApprovalWorkflow>.Filter.Eq("ConditionValue", value)
                                                    };

                                                    logs.AppendLine(string.Format("special origin value: {0}", value));
                                                    logs.AppendLine(string.Format("special new value: {0}", new_condition_values[i]));

                                                    workflows.FindOneAndUpdate<ApprovalWorkflow>(Builders<ApprovalWorkflow>.Filter.And(filters),
                                                        Builders<ApprovalWorkflow>.Update
                                                        .Set("ConditionValue", new_condition_values[i]));
                                                }
                                                values = new_condition_values;
                                            }
                                        }

                                        foreach (var value in values)
                                        {
                                            var new_value_chunks = new List<string>();
                                            var value_chunks = value.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);

                                            foreach (var value_chunk in value_chunks)
                                            {
                                                var _value_chunk = regex_tw.Replace(value_chunk, "");

                                                if (_value_chunk.Contains("<="))
                                                {
                                                    new_value_chunks.Add(regex_id_ir.Replace(_value_chunk, "<=" + id));
                                                }
                                                else
                                                {
                                                    new_value_chunks.Add(id + regex_id_il.Match(_value_chunk).Value);
                                                }
                                            }
                                            tpl_new_values.Add(string.Join(" && ", new_value_chunks));

                                            var filters = new List<FilterDefinition<ApprovalWorkflow>>() {
                                                Builders<ApprovalWorkflow>.Filter.Eq("FormGuid", condition_tpl.FormGuid),
                                                Builders<ApprovalWorkflow>.Filter.Eq("ConditionValue", value)
                                            };

                                            workflows.FindOneAndUpdate<ApprovalWorkflow>(Builders<ApprovalWorkflow>.Filter.And(filters),
                                                Builders<ApprovalWorkflow>.Update
                                                .Set("ConditionValue", string.Join(" && ", new_value_chunks)));
                                        }

                                        logs.AppendLine(string.Format("origin id: {0} ", condition_tpl.ConditionName));

                                        condition_tpl.ConditionName = id;

                                        logs.AppendLine(string.Format("new id: {0} ", condition_tpl.ConditionName));


                                        condition_tpl.ConditionValues = string.Join(",", tpl_new_values);
                                        logs.AppendLine(string.Format("new values: {0} ", string.Join(",", tpl_new_values)));
                                    }
                                    #endregion
                                }
                                #endregion

                                #region 替换json中的id
                                {
                                    if (regex_json_id.IsMatch(condition_tpl.FormJson))
                                    {
                                        logs.AppendLine(string.Format("origin json: {0} ", condition_tpl.FormJson));

                                        var tpl_json_idkv = regex_json_id.Matches(condition_tpl.FormJson);
                                        foreach (var tpl_json_id in tpl_json_idkv)
                                        {
                                            var json_id = tpl_json_id.ToString();
                                            if (!regex_digit.IsMatch(json_id)) continue;

                                            var json_id_value = regex_digit.Match(json_id).Value;
                                            condition_tpl.FormJson = condition_tpl.FormJson.Replace(json_id, "\"id\":\"" + control_id_prefix + json_id_value + "\",");
                                        }

                                        logs.AppendLine(string.Format("new json: {0} ", condition_tpl.FormJson));
                                    }
                                }
                                #endregion

                                tpls.FindOneAndReplace<FormTemplate>(Builders<FormTemplate>.Filter.Eq("_id", condition_tpl._id), condition_tpl);
                            }

                            //删除[IsHasCondition]字段
                            workflows.UpdateMany<ApprovalWorkflow>((m) => m.IsHasCondition || !m.IsHasCondition, Builders<ApprovalWorkflow>.Update.Unset((m) => m.IsHasCondition));
                        }

                        Logger.Instance.Log(logs.ToString());

                        return;
                    }
                    #endregion

                    #region 基础数据

                    #region 旧审批JSON数据

                    var form_jsons = new Dictionary<EnumFormType, string>()
                    {
                        { EnumFormType.Reimburse,"{\"title\":\"报销\",\"abstact\":\"适用于公司报销审批\",\"icon\":\"/content/img/approvalIcon/approvalIcon_3.png\",\"fields\":[{\"cid\":\"c17\",\"field_type\":\"group\",\"required\":\"false\",\"field_options\":{},\"label\":\"明细\",\"describe\":\"增加报销明细\",\"group\":[{\"cid\":\"c6\",\"field_type\":\"price\",\"required\":\"true\",\"field_options\":{\"description\":\"请填写需要报销的总金额\",\"canTotal\":true},\"label\":\"报销金额（元）\",\"describe\":\"请输入数字\",\"capital\":\"true\",\"isSub\":true,\"pCid\":\"c17\"},{\"cid\":\"c2\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"办公费\",\"checked\":true},{\"label\":\"差旅费\",\"checked\":false},{\"label\":\"培训费\",\"checked\":false},{\"label\":\"招待费\",\"checked\":false},{\"label\":\"房租水电费\",\"checked\":false},{\"label\":\"其他费用\",\"checked\":false}]},\"label\":\"报销类型\",\"describe\":\"如：办公费等\",\"isSub\":true,\"pCid\":\"c17\"},{\"cid\":\"c12\",\"field_type\":\"paragraph\",\"required\":\"false\",\"field_options\":{\"size\":\"small\"},\"label\":\"费用明细\",\"describe\":\"请输入费用明细\",\"isSub\":true,\"pCid\":\"c17\"}]},{\"cid\":\"c18\",\"field_type\":\"pic\",\"required\":\"true\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.Contract, "{\"title\":\"合同审批\",\"abstact\":\"适用于草拟合同以后的审批\",\"icon\":\"/content/img/approvalIcon/approvalIcon_7.png\",\"fields\":[{\"cid\":\"c2\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"采购合同\",\"checked\":true},{\"label\":\"销售合同\",\"checked\":false},{\"label\":\"技术合同\",\"checked\":false},{\"label\":\"其他\",\"checked\":false}]},\"label\":\"合同类型\",\"describe\":\"请选择\"},{\"cid\":\"c6\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{\"size\":\"small\"},\"label\":\"合同编号\",\"describe\":\"如：LTKXXD160429-001\"},{\"cid\":\"c17\",\"field_type\":\"price\",\"required\":\"true\",\"field_options\":{},\"label\":\"金额(元)\",\"describe\":\"请输入数字\",\"capital\":\"true\"},{\"cid\":\"c10\",\"field_type\":\"date\",\"required\":\"true\",\"field_options\":{\"date_type\":\"1\"},\"label\":\"签订时间\",\"describe\":\"\"},{\"cid\":\"c14\",\"field_type\":\"paragraph\",\"required\":\"true\",\"field_options\":{\"size\":\"small\",\"description\":\"适用于草拟合同以后的审批\"},\"label\":\"详细说明\",\"describe\":\"请输入详细说明\"},{\"cid\":\"c19\",\"field_type\":\"pic\",\"required\":\"true\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.Borrow, "{\"title\":\"借款\",\"abstact\":\"适用于向公司借款审批\",\"icon\":\"/content/img/approvalIcon/approvalIcon_5.png\",\"fields\":[{\"cid\":\"c2\",\"field_type\":\"price\",\"required\":\"true\",\"field_options\":{},\"label\":\"借款金额\",\"describe\":\"请输入数字\",\"capital\":\"true\"},{\"cid\":\"c6\",\"field_type\":\"paragraph\",\"required\":\"true\",\"field_options\":{\"size\":\"small\"},\"label\":\"借款原因\",\"describe\":\"请输入借款原因\"},{\"cid\":\"c10\",\"field_type\":\"date\",\"required\":\"true\",\"field_options\":{\"date_type\":0},\"label\":\"借款时间\",\"describe\":\"\"},{\"cid\":\"c14\",\"field_type\":\"date\",\"required\":\"true\",\"field_options\":{\"date_type\":0},\"label\":\"归还时间\",\"describe\":\"\"},{\"cid\":\"c19\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.AskForLeave, "{\"title\":\"请假\",\"abstact\":\"适用于请假审批\",\"icon\":\"/content/img/approvalIcon/approvalIcon_1.png\",\"fields\":[{\"cid\":\"c2\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"事假\",\"checked\":true},{\"label\":\"病假\",\"checked\":false},{\"label\":\"年假\",\"checked\":false},{\"label\":\"调休\",\"checked\":false},{\"label\":\"婚假\",\"checked\":false},{\"label\":\"产假\",\"checked\":false},{\"label\":\"陪产假\",\"checked\":false},{\"label\":\"路途假\",\"checked\":false},{\"label\":\"丧假\",\"checked\":false},{\"label\":\"带薪假\",\"checked\":false},{\"label\":\"其他\",\"checked\":false}]},\"label\":\"请假类型\",\"describe\":\"如：事假，病假等\"},{\"cid\":\"c19\",\"field_type\":\"datearea\",\"required\":\"true\",\"field_options\":{\"date_type\":\"1\",\"label2\":\"结束时间\"},\"label\":\"开始时间\",\"describe\":\"请选择\"},{\"cid\":\"c17\",\"field_type\":\"number\",\"required\":\"true\",\"field_options\":{\"unit\":\"\"},\"label\":\"请假天数\",\"describe\":\"请输入数字\"},{\"cid\":\"c16\",\"field_type\":\"paragraph\",\"required\":\"true\",\"field_options\":{\"size\":\"small\",\"description\":\"请输入请假事由\"},\"label\":\"请假事由\",\"describe\":\"请输入请假事由\"},{\"cid\":\"c18\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.Request, "{\"title\":\"请示\",\"abstact\":\"适用于向上级请示工作\",\"icon\":\"/content/img/approvalIcon/approvalIcon_6.png\",\"fields\":[{\"cid\":\"c2\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{\"size\":\"small\"},\"label\":\"请示主题\",\"describe\":\"请输入\"},{\"cid\":\"c6\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"一般\",\"checked\":true},{\"label\":\"紧急\",\"checked\":false},{\"label\":\"非常紧急\",\"checked\":false}]},\"label\":\"紧急度\",\"describe\":\"请选择\"},{\"cid\":\"c10\",\"field_type\":\"paragraph\",\"required\":\"true\",\"field_options\":{\"size\":\"small\"},\"label\":\"详细说明\",\"describe\":\"请输入\"},{\"cid\":\"c15\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.FieldWork, "{\"title\":\"外勤\",\"abstact\":\"适用于外勤审批\",\"icon\":\"/content/img/approvalIcon/approvalIcon_2.png\",\"fields\":[{\"cid\":\"c17\",\"field_type\":\"datearea\",\"required\":\"true\",\"field_options\":{\"date_type\":1,\"label2\":\"结束时间\"},\"label\":\"开始时间\",\"describe\":\"请选择\"},{\"cid\":\"c6\",\"field_type\":\"number\",\"required\":\"true\",\"field_options\":{\"description\":\"请输入外勤小时数\",\"unit\":\"（天）\"},\"label\":\"外勤时间\",\"describe\":\"请输入数字\"},{\"cid\":\"c12\",\"field_type\":\"paragraph\",\"required\":\"true\",\"field_options\":{\"size\":\"small\",\"description\":\"请输入外勤事由\"},\"label\":\"外勤事由\",\"describe\":\"请输入外勤理由\"},{\"cid\":\"c18\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.Purchase, "{\"title\":\"物品采购\",\"abstact\":\"用于企业办公或所需材料的采购申请\",\"icon\":\"/content/img/approvalIcon/approvalIcon_9.png\",\"fields\":[{\"cid\":\"c2\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"办公用品\",\"checked\":true},{\"label\":\"生活用品\",\"checked\":false},{\"label\":\"生产材料\",\"checked\":false},{\"label\":\"其他\",\"checked\":false}]},\"label\":\"物品类型\",\"describe\":\"如：办公用品等\"},{\"cid\":\"c14\",\"field_type\":\"date\",\"required\":\"true\",\"field_options\":{\"date_type\":0},\"label\":\"期望交付日期\",\"describe\":\"\"},{\"cid\":\"c19\",\"field_type\":\"group\",\"required\":\"false\",\"field_options\":{},\"label\":\"采购明细\",\"describe\":\"添加采购明细\",\"group\":[{\"cid\":\"c20\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{},\"label\":\"名称\",\"describe\":\"请输入\",\"isSub\":true,\"pCid\":\"c19\"},{\"cid\":\"c21\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{},\"label\":\"规格\",\"describe\":\"请输入\",\"isSub\":true,\"pCid\":\"c19\"},{\"cid\":\"c22\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{},\"label\":\"数量\",\"describe\":\"请输入\",\"isSub\":true,\"pCid\":\"c19\"},{\"cid\":\"c23\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{},\"label\":\"单位\",\"describe\":\"请输入\",\"isSub\":true,\"pCid\":\"c19\"},{\"cid\":\"c24\",\"field_type\":\"price\",\"required\":\"true\",\"field_options\":{\"canTotal\":true},\"label\":\"金额(元)\",\"describe\":\"请输入\",\"capital\":\"true\",\"isSub\":true,\"pCid\":\"c19\"}]},{\"cid\":\"c18\",\"field_type\":\"paragraph\",\"required\":\"false\",\"field_options\":{\"size\":\"small\"},\"label\":\"备注\",\"describe\":\"\"},{\"cid\":\"c25\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.SuppliesApplication, "{\"title\":\"物品领用\",\"abstact\":\"适用于物品领用申请\",\"icon\":\"/content/img/approvalIcon/approvalIcon_4.png\",\"fields\":[{\"cid\":\"c2\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"办公用品\",\"checked\":true},{\"label\":\"生活用品\",\"checked\":false},{\"label\":\"生产材料\",\"checked\":false},{\"label\":\"其他\",\"checked\":false}]},\"label\":\"物品用途\",\"describe\":\"请选择\"},{\"cid\":\"c23\",\"field_type\":\"group\",\"required\":\"false\",\"field_options\":{},\"label\":\"明细\",\"describe\":\"增加物品明细\",\"group\":[{\"cid\":\"c24\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{},\"label\":\"物品名称\",\"describe\":\"请输入\",\"isSub\":true,\"pCid\":\"c23\"},{\"cid\":\"c25\",\"field_type\":\"number\",\"required\":\"true\",\"field_options\":{\"unit\":\"\"},\"label\":\"数量\",\"describe\":\"请输入\",\"isSub\":true,\"pCid\":\"c23\"}]},{\"cid\":\"c14\",\"field_type\":\"paragraph\",\"required\":\"true\",\"field_options\":{\"size\":\"small\",\"description\":\"请输入物品领用详情说明\"},\"label\":\"领用详情\",\"describe\":\"请输入详细说明\"},{\"cid\":\"c26\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"附件\",\"describe\":\"请输入\"}]}" },
                        { EnumFormType.UseSeal, "{\"title\":\"用印申请\",\"abstact\":\"适用于公司印章使用\",\"icon\":\"/content/img/approvalIcon/approvalIcon_8.png\",\"fields\":[{\"cid\":\"c38\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"产品部\",\"checked\":false},{\"label\":\"市场部\",\"checked\":false},{\"label\":\"总经办\",\"checked\":false}]},\"label\":\"用印部门\",\"describe\":\"请选择\"},{\"cid\":\"c35\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{},\"label\":\"经办人\",\"describe\":\"请输入\"},{\"cid\":\"c10\",\"field_type\":\"date\",\"required\":\"true\",\"field_options\":{\"date_type\":\"1\"},\"label\":\"使用时间\",\"describe\":\"\"},{\"cid\":\"c14\",\"field_type\":\"text\",\"required\":\"true\",\"field_options\":{\"size\":\"small\",\"description\":\"请填写用印文件名称\"},\"label\":\"文件名称\",\"describe\":\"请输入\"},{\"cid\":\"c20\",\"field_type\":\"number\",\"required\":\"true\",\"field_options\":{\"description\":\"请填写文件份数\"},\"label\":\"文件份数\",\"describe\":\"请输入\"},{\"cid\":\"c24\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"公告类\",\"checked\":true},{\"label\":\"规章制度类\",\"checked\":false},{\"label\":\"合同类\",\"checked\":false},{\"label\":\"其他\",\"checked\":false}]},\"label\":\"文件类别\",\"describe\":\"请选择\"},{\"cid\":\"c6\",\"field_type\":\"radio\",\"required\":\"true\",\"field_options\":{\"options\":[{\"label\":\"公章\",\"checked\":true},{\"label\":\"合同章\",\"checked\":false},{\"label\":\"法人章\",\"checked\":false},{\"label\":\"其他\",\"checked\":false}]},\"label\":\"印章类型\",\"describe\":\"请选择\"},{\"cid\":\"c28\",\"field_type\":\"paragraph\",\"required\":\"false\",\"field_options\":{\"size\":\"small\",\"description\":\"请填写详细说明\"},\"label\":\"备注\",\"describe\":\"请填写备注\"},{\"cid\":\"c39\",\"field_type\":\"pic\",\"required\":\"false\",\"field_options\":{},\"label\":\"图片\",\"describe\":\"请输入\"}]}" }
                    };

                    #endregion

                    #region 新审批JSON数据

                    var new_form_jsons = new Dictionary<EnumFormType, string>()
                    {
                        { EnumFormType.Reimburse,"{\"title\":\"报销\",\"abstact\":\"适用于公司报销审批\",\"fields\":[{\"name\":\"明细\",\"type\":\"group\",\"children\":[{\"name\":\"报销金额\",\"type\":\"price\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请填写需要报销的总金额\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true},{\"name\":\"报销类型\",\"type\":\"radio\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"如：办公费等\",\"isRequired\":true,\"optionItems\":[{\"text\":\"办公费\"},{\"text\":\"差旅费\"},{\"text\":\"培训费\"},{\"text\":\"招待费\"},{\"text\":\"房租水电费\"},{\"text\":\"其他费用\"}]},\"val\":\"\",\"isInGroup\":true},{\"name\":\"费用明细\",\"type\":\"paragraph\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入费用明细\",\"isRequired\":false},\"val\":\"\",\"isInGroup\":true}],\"isClick\":false,\"total\":[{\"name\":\"报销金额\",\"type\":\"price\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请填写需要报销的总金额\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}],\"option\":{\"btnText\":\"增加报销明细\"},\"id\":0,\"isInGroup\":false,\"clone\":[{\"name\":\"明细\",\"items\":[{\"name\":\"报销金额\",\"type\":\"price\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请填写需要报销的总金额\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true},{\"name\":\"报销类型\",\"type\":\"radio\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"如：办公费等\",\"isRequired\":true,\"optionItems\":[{\"text\":\"办公费\"},{\"text\":\"差旅费\"},{\"text\":\"培训费\"},{\"text\":\"招待费\"},{\"text\":\"房租水电费\"},{\"text\":\"其他费用\"}]},\"val\":\"\",\"isInGroup\":true},{\"name\":\"费用明细\",\"type\":\"paragraph\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入费用明细\",\"isRequired\":false},\"val\":\"\",\"isInGroup\":true}]}]},{\"name\":\"附件\",\"type\":\"file\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_3.png\",\"isNewVersion\":true,\"maxId\":5}" },
                            { EnumFormType.Contract, "{\"title\":\"合同审批\",\"abstact\":\"适用于草拟合同以后的审批\",\"fields\":[{\"name\":\"合同类型\",\"type\":\"radio\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"optionItems\":[{\"text\":\"采购合同\"},{\"text\":\"销售合同\"},{\"text\":\"技术合同\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"合同编号\",\"type\":\"text\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"如：LTKXXD160429-001\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"金额\",\"type\":\"price\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请输入数字\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":false},{\"name\":\"签订时间\",\"type\":\"date\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":false},\"val\":\"\",\"isInGroup\":false},{\"name\":\"详细说明\",\"type\":\"paragraph\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入详细说明\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件\",\"type\":\"file\",\"id\":5,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_7.png\",\"isNewVersion\":true,\"maxId\":6}" },
                            { EnumFormType.Borrow, "{\"title\":\"借款\",\"abstact\":\"适用于向公司借款审批\",\"fields\":[{\"name\":\"借款金额\",\"type\":\"price\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"请输入数字\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":false},{\"name\":\"借款原因\",\"type\":\"paragraph\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请输入借款原因\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"借款起止\",\"type\":\"datearea\",\"id\":5,\"isClick\":false,\"option\":{\"name1\":\"开始时间\",\"name2\":\"结束时间\",\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":false,\"hasTotal\":true,\"isCompute\":true,\"computeTitle\":\"时长\",\"computeVal\":\"\",\"isAssociatedTimecard\":false,\"associatedTimecardType\":\"0\"},\"val\":{\"begin\":\"\",\"end\":\"\"},\"isInGroup\":false},{\"name\":\"附件0\",\"type\":\"file\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_5.png\",\"isNewVersion\":true,\"maxId\":6}" },
                            { EnumFormType.AskForLeave, "{\"title\":\"请假\",\"abstact\":\"适用于请假审批\",\"fields\":[{\"name\":\"请假类型\",\"type\":\"radio\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"如：事假，病假等\",\"isRequired\":true,\"optionItems\":[{\"text\":\"事假\"},{\"text\":\"病假\"},{\"text\":\"年假\"},{\"text\":\"调休\"},{\"text\":\"婚假\"},{\"text\":\"产假\"},{\"text\":\"陪产假\"},{\"text\":\"路途假\"},{\"text\":\"丧假\"},{\"text\":\"孕检假\"},{\"text\":\"特殊情况请假\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"请假时间\",\"type\":\"datearea\",\"id\":1,\"isClick\":false,\"option\":{\"name1\":\"开始时间\",\"name2\":\"结束时间\",\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":true,\"hasTotal\":true,\"isCompute\":true,\"computeTitle\":\"时长\",\"computeVal\":\"\",\"isAssociatedTimecard\":false,\"associatedTimecardType\":\"0\"},\"val\":{\"begin\":\"\",\"end\":\"\"},\"isInGroup\":false},{\"name\":\"请假事由\",\"type\":\"paragraph\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请输入请假事由\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件\",\"type\":\"file\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_1.png\",\"isNewVersion\":true,\"maxId\":4}" },
                            { EnumFormType.Request, "{\"title\":\"请示\",\"abstact\":\"适用于向上级请示工作\",\"fields\":[{\"name\":\"请示主题\",\"type\":\"text\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"紧急度\",\"type\":\"radio\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"optionItems\":[{\"text\":\"一般\"},{\"text\":\"紧急\"},{\"text\":\"非常紧急\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"详细说明\",\"type\":\"paragraph\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件0\",\"type\":\"file\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_1.png\",\"isNewVersion\":true,\"maxId\":4}" },
                            { EnumFormType.FieldWork, "{\"title\":\"外勤\",\"abstact\":\"适用于外勤审批\",\"fields\":[{\"name\":\"外勤起止\",\"type\":\"datearea\",\"id\":0,\"isClick\":false,\"option\":{\"name1\":\"开始时间\",\"name2\":\"结束时间\",\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":true,\"hasTotal\":true,\"isCompute\":true,\"computeTitle\":\"时长\",\"computeVal\":\"\",\"isAssociatedTimecard\":false,\"associatedTimecardType\":\"0\"},\"val\":{\"begin\":\"\",\"end\":\"\"},\"isInGroup\":false},{\"name\":\"外勤事由\",\"type\":\"paragraph\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请输入外勤事由\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件\",\"type\":\"file\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_15.png\",\"isNewVersion\":true,\"maxId\":3}" },
                            { EnumFormType.Purchase, "{\"title\":\"物品采购\",\"abstact\":\"用于企业办公或所需材料的采购申请\",\"fields\":[{\"name\":\"物品类型\",\"type\":\"radio\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"如：办公用品等\",\"isRequired\":true,\"optionItems\":[{\"text\":\"办公用品\"},{\"text\":\"生活用品\"},{\"text\":\"生产材料\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"期望交付日期\",\"type\":\"date\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":false},\"val\":\"\",\"isInGroup\":false},{\"name\":\"明细0\",\"type\":\"group\",\"children\":[{\"name\":\"名称\",\"type\":\"text\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"规格\",\"type\":\"text\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"数量\",\"type\":\"number\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"单位\",\"type\":\"text\",\"id\":7,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"金额\",\"type\":\"price\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}],\"isClick\":false,\"total\":[{\"name\":\"数量\",\"type\":\"number\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"金额\",\"type\":\"price\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}],\"option\":{\"btnText\":\"添加采购明细\"},\"id\":2,\"isInGroup\":false,\"clone\":[{\"name\":\"明细0\",\"items\":[{\"name\":\"名称\",\"type\":\"text\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"规格\",\"type\":\"text\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"数量\",\"type\":\"number\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"单位\",\"type\":\"text\",\"id\":7,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"金额\",\"type\":\"price\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}]}]},{\"name\":\"备注\",\"type\":\"paragraph\",\"id\":9,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":false},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件\",\"type\":\"file\",\"id\":10,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_9.png\",\"isNewVersion\":true,\"maxId\":11}" },
                            { EnumFormType.SuppliesApplication, "{\"title\":\"物品领用\",\"abstact\":\"适用于物品领用申请\",\"fields\":[{\"name\":\"物品用途\",\"type\":\"radio\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"optionItems\":[{\"text\":\"办公用品\"},{\"text\":\"生活用品\"},{\"text\":\"生产材料\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"明细\",\"type\":\"group\",\"children\":[{\"name\":\"物品名称\",\"type\":\"text\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"数量\",\"type\":\"number\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"领用详情\",\"type\":\"paragraph\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入详细说明\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true}],\"isClick\":false,\"total\":[{\"name\":\"数量\",\"type\":\"number\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true}],\"option\":{\"btnText\":\"增加物品明细\"},\"id\":1,\"isInGroup\":false,\"clone\":[{\"name\":\"明细\",\"items\":[{\"name\":\"物品名称\",\"type\":\"text\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"数量\",\"type\":\"number\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"领用详情\",\"type\":\"paragraph\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入详细说明\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true}]}]},{\"name\":\"附件\",\"type\":\"file\",\"id\":5,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_4.png\",\"isNewVersion\":true,\"maxId\":6}" },
                            { EnumFormType.UseSeal, "{\"title\":\"用印申请\",\"abstact\":\"适用于公司印章使用\",\"fields\":[{\"name\":\"用印部门\",\"type\":\"radio\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"optionItems\":[{\"text\":\"产品部\"},{\"text\":\"市场部\"},{\"text\":\"研发部\"},{\"text\":\"总经办\"},{\"text\":\"行政人事部\"},{\"text\":\"财务部\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"经办人\",\"type\":\"text\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"使用时间\",\"type\":\"date\",\"id\":2,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"文件名称\",\"type\":\"text\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请填写用印文件名称\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":false},{\"name\":\"文件份数\",\"type\":\"number\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请填写文件份数\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":false},{\"name\":\"文件类别\",\"type\":\"radio\",\"id\":5,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"optionItems\":[{\"text\":\"公告类\"},{\"text\":\"规章制度类\"},{\"text\":\"合同类\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"印章类型\",\"type\":\"radio\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"optionItems\":[{\"text\":\"公章\"},{\"text\":\"合同章\"},{\"text\":\"法人章\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"备注\",\"type\":\"paragraph\",\"id\":7,\"isClick\":false,\"option\":{\"placeholder\":\"请填写备注\",\"isRequired\":false},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件\",\"type\":\"file\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_8.png\",\"isNewVersion\":true,\"maxId\":9}" }
                        };

                    #endregion

                    var origin_form_jsons = form_jsons.Select(q => q.Value)
                        .ToList();

                    var origin_form_id = form_jsons.Select(q => q.Key)
                        .ToList();

                    var reset_companies = new List<int> {
                            10001,10006,10008,10018,
                            10023,10298,10971,11777,11783,
                            11808,11947,11983,12289,
                            12379,12382,12439,12440,12504,
                            12505,12531,12575,12770
                        };

                    #endregion

                    #region Step1/统计数据
                    if (false)
                    {
                        #region 统计审批记录数量

                        long total_records = 0;  //审批记录数量
                        long total_utpl = 0; //用户模板
                        long total_exists_record_company = 0;
                        foreach (var company in companies)
                        {
                            var mongoDb = mongoClient.GetDatabase(String.Concat(debug_prefix, "company_", company));
                            logs.AppendLine(String.Format("圈子:{0}", company));

                            var tpls = mongoDb.GetCollection<FormTemplate>("FormTemplate");

                            var records = mongoDb.GetCollection<FormRecord>("FormRecord")
                                .Count(Builders<FormRecord>.Filter.Empty);

                            var utpls = tpls.Count(Builders<FormTemplate>.Filter.Eq<int>("FormTemplateType", 1));

                            if (utpls > 0)
                            {
                                total_utpl += utpls;
                            }

                            if (records > 0)
                            {
                                ++total_exists_record_company;
                                total_records += records;
                                logs.AppendLine(String.Format("圈子{0}存在{1}条用户模板, {2}条审批数据.", company, utpls, records));
                            }
                        }

                        logs.AppendLine(String.Format("总计{0}条用户模板.共计{1}个圈子存在审批数据.", total_utpl, total_exists_record_company));

                        #endregion

                        #region 重置指定圈子的模板

                        #region 重置非指定圈子的模板
                        {
                            var reset_unused_companies = companies.Where(w => !reset_companies.Contains(w));

                            logs.AppendLine(String.Format("重置非指定圈子的模板 begin"));
                            foreach (var item in reset_unused_companies)
                            {
                                logs.AppendLine(String.Format("圈子:{0}", item));

                                var mongoDb = mongoClient.GetDatabase(String.Concat(debug_prefix, "company_", item));
                                var tpls = mongoDb.GetCollection<FormTemplate>("FormTemplate");

                                var unused_tpls = tpls.Find(w => origin_form_id.Contains(w.FormType))
                                    .ToList();

                                if (unused_tpls.Count > 0)
                                {
                                    logs.AppendLine(String.Format("重置非指定圈子的模板【圈子{0}】begin", item));

                                    foreach (var tpl in unused_tpls)
                                    {
                                        logs.AppendLine(String.Format("[DEBUG] 旧模板：{0}", tpl.FormJson));
                                        logs.AppendLine(String.Format("[DEBUG] 新模板：{0}", new_form_jsons[tpl.FormType]));

                                        tpl.FormJson = new_form_jsons[tpl.FormType];
                                        tpls.FindOneAndReplace<FormTemplate>(Builders<FormTemplate>.Filter.Eq("_id", tpl._id), tpl);

                                        logs.AppendLine(String.Format("模板已替换：{0}", tpl.FormName));
                                    }

                                    logs.AppendLine(String.Format("重置非指定圈子的模板【圈子{0}】end", item));
                                    logs.AppendLine();
                                }
                            }

                            logs.AppendLine(String.Format("重置非指定圈子的模板 end"));
                            logs.AppendLine();
                        }

                        #endregion

                        #region 重置指定圈子的模板
                        {
                            logs.AppendLine(String.Format("重置指定圈子的模板 begin"));

                            foreach (var item in reset_companies)
                            {
                                logs.AppendLine(String.Format("圈子:{0}", item));

                                var mongoDb = mongoClient.GetDatabase(String.Concat(debug_prefix, "company_", item));
                                var tpls = mongoDb.GetCollection<FormTemplate>("FormTemplate");

                                #region 包含条件的审批
                                {
                                    var condition_tpls = tpls.Find(w => w.IsCondition)
                                        .ToList();

                                    if (condition_tpls.Count > 0)
                                    {
                                        logs.AppendLine(String.Format("设定的审批条件："));
                                        foreach (var tpl in condition_tpls)
                                        {
                                            logs.AppendLine(String.Format("ObjectId:{0} Title：{1}", tpl._id, tpl.FormName));
                                        }
                                    }
                                }
                                #endregion

                                #region 更改过模板的审批
                                {
                                    var modified_tpls = tpls.Find(w => origin_form_id.Contains(w.FormType) && !origin_form_jsons.Contains(w.FormJson))
                                        .ToList();

                                    if (modified_tpls.Count > 0)
                                    {
                                        logs.AppendLine(String.Format("更改过模板的审批 begin"));

                                        foreach (var doc in modified_tpls)
                                        {
                                            logs.AppendLine(String.Format("ObjectId:{0} Title：{1}", doc._id, doc.FormName));
                                        }

                                        logs.AppendLine(String.Format("更改过模板的审批 end"));
                                        logs.AppendLine();
                                    }

                                }
                                #endregion

                                #region 替换未修改过的模板
                                {
                                    var origin_tpls = tpls.Find(w => origin_form_jsons.Contains(w.FormJson))
                                        .ToList();

                                    if (origin_tpls.Count > 0)
                                    {
                                        logs.AppendLine(String.Format("替换未修改过的模板 begin"));
                                        foreach (var tpl in origin_tpls)
                                        {
                                            logs.AppendLine(String.Format("[DEBUG] 旧模板：{0}", tpl.FormJson));
                                            logs.AppendLine(String.Format("[DEBUG] 新模板：{0}", new_form_jsons[tpl.FormType]));

                                            tpl.FormJson = new_form_jsons[tpl.FormType];
                                            tpls.FindOneAndReplace<FormTemplate>(Builders<FormTemplate>.Filter.Eq("_id", tpl._id), tpl);
                                            logs.AppendLine(String.Format("模板已替换：{0}", tpl.FormName));
                                        }

                                        logs.AppendLine(String.Format("替换未修改过的模板 end"));
                                        logs.AppendLine();
                                    }
                                }
                                #endregion
                            }

                            logs.AppendLine(String.Format("重置指定圈子的模板 end"));
                        }

                        #endregion

                        #endregion
                    }
                    #endregion

                    #region Setp2/处理数据
                    {
                        #region 替换指定名称的审批
                        {
                            foreach (var company in companies)
                            {
                                logs.AppendLine(String.Format("圈子:{0}", company));

                                var mongoDb = mongoClient.GetDatabase(String.Concat(debug_prefix, "company_", company));
                                var tpls = mongoDb.GetCollection<FormTemplate>("FormTemplate");
                                var workflows = mongoDb.GetCollection<ApprovalWorkflow>("ApprovalWorkflow");

                                var origin_tpls = tpls.Find(w => origin_form_id.Contains(w.FormType))
                                    .ToList();

                                foreach (var tpl in origin_tpls)
                                {
                                    tpl.FormJson = new_form_jsons[tpl.FormType];
                                    tpls.FindOneAndReplace<FormTemplate>(Builders<FormTemplate>.Filter.Eq("_id", tpl._id), tpl);
                                    logs.AppendLine(String.Format("模板已替换：{0}", tpl.FormName));
                                }

                                var directory = new DirectoryInfo(@"./DbTest/Data/" + company);
                                if (!directory.Exists)
                                {
                                    logs.AppendLine(string.Format("未搜索到编号为{0}的圈子审批数据", company));
                                    continue;
                                }

                                logs.AppendLine();
                                logs.AppendLine(string.Format("处理{0}的圈子审批数据", company));

                                var directory_files = directory.GetFiles("*.json");

                                foreach (var directory_file in directory_files)
                                {
                                    using (var sr = new StreamReader(directory_file.FullName, Encoding.UTF8))
                                    {
                                        var obj = JsonConvert.DeserializeObject<JSONData>(sr.ReadToEnd());

                                        var obj_name = directory_file.Name.Replace(directory_file.Extension, "");

                                        var form_tpls = tpls.Find(w => obj_name.Equals(w.FormName))
                                            .ToList();

                                        if (form_tpls.Count == 0) continue;

                                        foreach (var tpl in form_tpls)
                                        {
                                            tpl.FormJson = obj.formjson;

                                            if (obj.condition == null || obj.condition.values.Count == 0)
                                            {
                                                tpl.ConditionName = string.Empty;
                                                tpl.ConditionValues = string.Empty;
                                                tpl.IsCondition = false;

                                                //更新tpl
                                                tpls.FindOneAndReplace<FormTemplate>(Builders<FormTemplate>.Filter.Eq("_id", tpl._id), tpl);
                                                logs.AppendLine(string.Format("更新指定模板成功[Condition Is Null] ObjectId:{0}, Name:{1}, Value:{2}", tpl._id, tpl.ConditionName, tpl.ConditionValues));

                                                continue;
                                            }

                                            tpl.ConditionName = string.Join(",", obj.condition.cid);
                                            tpl.ConditionValues = string.Join(",", obj.condition.values);
                                            tpl.IsCondition = true;

                                            var tpl_workflows = workflows.Find(w => w.FormGuid == tpl.FormGuid && w.IsHasCondition)
                                                .ToList()
                                                .OrderBy(o => o.SortIndex)
                                                .ToList();

                                            if (tpl_workflows.Count != obj.condition.values.Count)
                                            {
                                                logs.AppendLine(string.Format("[条件数量不匹配] ObjectId:{0}, FormGuid:{1}, FormName:{2}; 实际条件数量:{3},提供的条件数量:{4};",
                                                    tpl._id,
                                                    tpl.FormGuid,
                                                    tpl.FormName,
                                                    tpl_workflows.Count,
                                                    obj.condition.values.Count));

                                                continue;
                                            }

                                            if (tpl_workflows != null && tpl_workflows.Any())
                                            {
                                                for (int i = 0; i < tpl_workflows.Count; i++)
                                                {
                                                    var tpl_workflow = tpl_workflows[i];
                                                    var tpl_workflow_condition = obj.condition.values[i];

                                                    tpl_workflow.ConditionName = tpl_workflow_condition.Replace(obj.condition.cid, obj.condition.name);
                                                    tpl_workflow.ConditionValue = tpl_workflow_condition;
                                                    tpl_workflow.HasCondition = true;
                                                    tpl_workflow.IsHasCondition = true;
                                                    tpl_workflow.SortIndex = i + 1;

                                                    //更新workflows节点
                                                    workflows.FindOneAndReplace<ApprovalWorkflow>(Builders<ApprovalWorkflow>.Filter.Eq("_id", tpl_workflow._id), tpl_workflow);

                                                    logs.AppendLine(string.Format("更新指定流程节点成功 ObjectId:{0}, Name:{1}, Value:{2}", tpl_workflow._id, tpl_workflow.ConditionName, tpl_workflow.ConditionValue));
                                                }
                                            }
                                            //更新tpl
                                            tpls.FindOneAndReplace<FormTemplate>(Builders<FormTemplate>.Filter.Eq("_id", tpl._id), tpl);
                                            logs.AppendLine(string.Format("更新指定模板成功 ObjectId:{0}, Name:{1}, Value:{2}", tpl._id, tpl.ConditionName, tpl.ConditionValues));
                                        }
                                    }
                                }

                                //删除[IsHasCondition]字段
                                workflows.UpdateMany<ApprovalWorkflow>((m) => m.IsHasCondition || !m.IsHasCondition, Builders<ApprovalWorkflow>.Update.Unset((m) => m.IsHasCondition));
                            }
                        }

                        #endregion
                    }
                    #endregion
                }
                #endregion

                Logger.Instance.Log(logs.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.Log("error：" + e.Message);
            }
        }


        public static string GetColumnName(Type type, string propertyName, DbContext context)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                          .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                      .Single()
                      .EntitySets
                      .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                          .Single()
                          .EntitySetMappings
                          .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var tableEntitySet = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            var tableName = tableEntitySet.MetadataProperties["Table"].Value ?? tableEntitySet.Name;

            // Find the storage property (column) that the property is mapped
            var columnName = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .PropertyMappings
                .OfType<ScalarPropertyMapping>()
                      .Single(m => m.Property.Name == propertyName)
                .Column
                .Name;

            return tableName + "." + columnName;
        }

        #region Entities

        [Serializable]
        public class JSONData
        {
            public string formjson { get; set; }
            public condition condition { get; set; }
        }

        [Serializable]
        public class condition
        {
            public condition()
            {
                this.values = new List<string>();
            }
            public string cid { get; set; }
            public string name { get; set; }
            public List<string> values { get; set; }
        }

        public class FormTemplate
        {
            public ObjectId _id { get; set; }
            public EnumFormType FormType { get; set; }
            public Guid FormGuid { get; set; }
            public string FormName { get; set; }
            public int FormTemplateType { get; set; }
            public string Icon { get; set; }
            public string FormDesignHtml { get; set; }
            public string FormShowHtml { get; set; }
            public string FormJson { get; set; }
            public Guid CreateUserGuid { get; set; }
            public int CreateUserNo { get; set; }
            public long CreateOnUtc { get; set; }
            public string Description { get; set; }
            public int CompanyNo { get; set; }
            public Guid EmployeeGuid { get; set; }
            public int Status { get; set; }
            public Nullable<Guid> OriginFormGuid { get; set; }
            public string Tag { get; set; }
            public int SortIndex { get; set; }
            public string ConditionName { get; set; }
            public string ConditionValues { get; set; }
            public bool IsCondition { get; set; }
            public bool? IsNewTemplate { get; set; }
            public bool? IsConnected { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class FormRecord
        {
            public ObjectId _id { get; set; }
            /// <summary>
            /// 表单模板编号
            /// </summary>
            public Guid FormGuid { get; set; }
            /// <summary>
            /// 系统模板编号(用于判断类型)
            /// </summary>
            public int FormType { get; set; }
            /// <summary>
            /// 表单名字
            /// </summary>
            public string FormName { get; set; }
            /// <summary>
            /// 表单一行记录的编号
            /// </summary>
            public Guid FormRecordGuid { get; set; }
            /// <summary>
            /// 设计表单
            /// </summary>
            public string FormDesignHtml { get; set; }
            /// <summary>
            /// 显示表单
            /// </summary>
            public string FormShowHtml { get; set; }
            /// <summary>
            /// 表单的JSON
            /// </summary>
            public string FormJson { get; set; }
            /// <summary>
            /// 表单图标
            /// </summary>
            public string FormIcon { get; set; }
            /// <summary>
            /// 创建者编号
            /// </summary>
            public Guid CreateUserGuid { get; set; }
            /// <summary>
            /// 创建者用户编号
            /// </summary>
            public int CreateUserNo { get; set; }
            /// <summary>
            /// 创建者姓名
            /// </summary>
            public string CreatorName { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public long CreateOnUtc { get; set; }
            /// <summary>
            /// 圈子编号
            /// </summary>
            public int CompanyNo { get; set; }
            /// <summary>
            /// 创建者员工编号（冗余）
            /// </summary>
            public Guid EmployeeGuid { get; set; }
            /// <summary>
            /// 表单状态
            /// </summary>
            public EnumFormStatus Status { get; set; }
            /// <summary>
            /// 本表单记录的流程编号
            /// </summary>
            public Guid WorkflowGuid { get; set; }
            /// <summary>
            /// 知悉人名字，逗号分隔
            /// </summary>
            public string MemberNames { get; set; }
            /// <summary>
            /// 知悉人Guid，逗号分隔
            /// </summary>
            public string Members { get; set; }
            /// <summary>
            /// 部门编号
            /// </summary>
            public int DepartmentId { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            public string DepartmentName { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 是否由新版模板创建
            /// </summary>
            public Nullable<bool> IsNewTemplate { get; set; }
            /// <summary>
            /// 审批完结时间
            /// <remarks>
            /// 完结指[撤销、拒绝、所有流程都被通过]
            /// </remarks>
            /// </summary>
            public Nullable<long> FinishedOnUtc { get; set; }

            #region 获取最新的流程审批状态(仅供程序判断使用)

            /// <summary>
            /// 审批最新状态
            /// </summary>
            [BsonIgnore]
            public EnumFormStatus LatestApprovalStatus { get; set; }
            /// <summary>
            /// 最新审批人名称
            /// </summary>
            [BsonIgnore]
            public string LatestManagerName { get; set; }

            #endregion
        }

        public class ApprovalWorkflow
        {
            public ObjectId _id { get; set; }
            /// <summary>
            /// 流程标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 流程编号
            /// </summary>
            //[BsonRepresentation(BsonType.String)]
            public Guid WorkflowGuid { get; set; }
            /// <summary>
            /// Description
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// 表单的GUID
            /// </summary>
            //[BsonRepresentation(BsonType.String)]
            public Guid? FormGuid { get; set; }
            /// <summary>
            /// CompanyNo
            /// </summary>
            public int CompanyNo { get; set; }
            /// <summary>
            /// Status
            /// </summary>
            public int Status { get; set; }
            /// <summary>
            /// CreateBy
            /// </summary>
            //[BsonRepresentation(BsonType.String)]
            public Guid CreateBy { get; set; }
            /// <summary>
            /// CreateOnUtc
            /// </summary>
            public long CreateOnUtc { get; set; }
            /// <summary>
            /// UpdateBy
            /// </summary>
            //[BsonRepresentation(BsonType.String)]
            public Nullable<Guid> UpdateBy { get; set; }

            /// <summary>
            /// UpdateOnUtc
            /// </summary>
            public long UpdateOnUtc { get; set; }

            /// <summary>
            /// 条件判断字段名称
            /// </summary>
            public string ConditionName { get; set; }

            /// <summary>
            /// 条件判断字段的值，如：1,3,5表示：<1,1<=a<3,3<=a<5,5<=a；事假,病假表示:a=事假或a=病假
            /// </summary>
            public string ConditionValue { get; set; }

            /// <summary>
            /// 是否分条件
            /// </summary>
            public bool HasCondition { get; set; }
            public bool IsHasCondition { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public int SortIndex { get; set; }
        }

        /// <summary>
        /// 表单类型
        /// </summary>
        public enum EnumFormType
        {
            /// <summary>
            /// 默认
            /// </summary>
            [Description("默认")]
            Default = 0,
            /// <summary>
            /// 报销
            /// </summary>
            [Description("报销")]
            Reimburse = 1,
            /// <summary>
            /// 合同
            /// </summary>
            [Description("合同")]
            Contract = 2,
            /// <summary>
            /// 借款
            /// </summary>
            [Description("借款")]
            Borrow = 3,
            /// <summary>
            /// 请假
            /// </summary>
            [Description("请假")]
            AskForLeave = 4,
            /// <summary>
            /// 外勤
            /// </summary>
            [Description("外勤")]
            FieldWork = 5,
            /// <summary>
            /// 采购
            /// </summary>
            [Description("采购")]
            Purchase = 6,
            /// <summary>
            /// 领用
            /// </summary>
            [Description("领用")]
            SuppliesApplication = 7,
            /// <summary>
            /// 用印
            /// </summary>
            [Description("用印")]
            UseSeal = 8,
            /// <summary>
            /// 出差
            /// </summary>
            [Description("出差")]
            BusinessTrip = 9,
            /// <summary>
            /// 加班
            /// </summary>
            [Description("加班")]
            WorkOvertime = 10,
            /// <summary>
            /// ")]
            /// </summary>
            [Description("入职")]
            Entry = 11,
            /// <summary>
            /// 离职
            /// </summary>
            [Description("离职")]
            Resign = 12,
            /// <summary>
            /// 请示
            /// </summary>
            [Description("请示")]
            Request = 13,
            /// <summary>
            /// 其它
            /// </summary>
            [Description("其它")]
            Other = 14

        }

        public class FormWorkflowRecord
        {
            public ObjectId _id { get; set; }
            /// <summary>
            /// 表单一行记录的编号
            /// </summary>
            public Guid FormRecordGuid { get; set; }
            /// <summary>
            /// FormGuid
            /// </summary>
            public Guid FormGuid { get; set; }
            /// <summary>
            /// FlowGuid
            /// </summary>
            public Guid WorkflowGuid { get; set; }
            /// <summary>
            /// CompanyNo
            /// </summary>
            public int CompanyNo { get; set; }
            /// <summary>
            /// Title
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// Description
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// Status
            /// </summary>
            public EnumFormStatus Status { get; set; }
            /// <summary>
            /// 审批人的员工编号
            /// </summary>
            public Guid ManagerGuid { get; set; }
            /// <summary>
            /// ManagerName
            /// </summary>
            public string ManagerName { get; set; }
            /// <summary>
            /// DepartmentId
            /// </summary>
            public int DepartmentId { get; set; }
            /// <summary>
            /// CreateBy
            /// </summary>
            public Guid CreateBy { get; set; }
            /// <summary>
            /// CreateName
            /// </summary>
            public string CreateName { get; set; }
            /// <summary>
            /// CreateOnUtc
            /// </summary>
            public long CreateOnUtc { get; set; }
            /// <summary>
            /// UpdateBy
            /// </summary>
            public Nullable<Guid> UpdateBy { get; set; }
            /// <summary>
            /// UpdateOnUtc
            /// </summary>
            public long UpdateOnUtc { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            public int SortIndex { get; set; }
        }

        public enum EnumFormStatus
        {
            /// <summary>
            /// 未提交
            /// </summary>
            [Description("未提交")]
            Uncommitted = 1,
            /// <summary>
            /// 待审批
            /// </summary>
            [Description("待审批")]
            WaitApproval = 2,
            /// <summary>
            /// 审批通过
            /// </summary>
            [Description("审批通过")]
            Pass = 3,
            /// <summary>
            /// 审批拒绝
            /// </summary>
            [Description("审批拒绝")]
            Refuse = 4,
            /// <summary>
            /// 在流程节点中，但还未到审批节点
            /// </summary>
            [Description("等待审批")]
            Pending = 5,
            /// <summary>
            /// 审批撤销
            /// </summary>
            [Description("审批撤销")]
            Cancel = 6
        }

        #endregion
    }
}
