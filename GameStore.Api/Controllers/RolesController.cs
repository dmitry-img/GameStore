using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {
        private IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IHttpActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();

            return Json(roles);
        }

        [HttpGet]
        [Route("paginated-list")]
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var roles = await _roleService.GetAllWithPaginationAsync(paginationDTO);

            return Json(roles);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);

            return Json(role);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleDTO createRoleDTO)
        {
            await _roleService.CreateAsync(createRoleDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdateRoleDTO updateRoleDTO)
        {
            await _roleService.UpdateAsync(id, updateRoleDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _roleService.DeleteAsync(id);

            return Ok();
        }
    }
}
