using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MVCAPIDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : ControllerBase
    {
        private string _conection = @"server=localhost;port=3306;database=basenet;uid=root;pwd=;";
        [HttpGet]
        //IACTIONRESULT permite regresar un status
        public IActionResult Get()
        {
            //permite crear listas a partir del modelo especificado
            IEnumerable<Models.Datos> lst = null;
            using (var db = new MySqlConnection(_conection))
            {
                var sql = "select * from datos";
                lst = db.Query<Models.Datos>(sql);

            }

            return Ok(lst);
            
        }

        [HttpPost]
        public IActionResult Insert(Models.Datos model)
        {
            int result=0;
            using (var db = new MySqlConnection(_conection))
            {
                var sql = "insert into datos(descripcion,comentario,algo)"+
                    " values(@descripcion,@comentario, @algo)";//evita inyeccion sql
                result = db.Execute(sql, model);
                
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Models.Datos model)
        {
            int result = 0;
            using (var db = new MySqlConnection(_conection))
            {
                var sql = "UPDATE datos set descripcion=@descripcion, comentario=@comentario, algo=@algo"+
                    " where id=@id";//evita inyeccion sql
                result = db.Execute(sql, model);

            }
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(Models.Datos model)
        {
            int result = 0;
            using (var db = new MySqlConnection(_conection))
            {
                var sql = "DELETE from datos where id=@id";//evita inyeccion sql
                result = db.Execute(sql, model);

            }
            return Ok(result);
        }
    }
}
