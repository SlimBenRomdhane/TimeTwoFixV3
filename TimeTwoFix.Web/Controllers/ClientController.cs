using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTwoFix.Application.ClientServices.Dtos;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.ClientModels;

namespace TimeTwoFix.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClientServices _clientServices;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper, IClientServices clientServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientServices = clientServices;
        }

        // GET: ClientController
        public async Task<IActionResult> Index(int pageNumber, int pageSize)
        {
            var clients = await _clientServices.GetAllAsyncServiceGeneric();
            ////Apply pagination
            //var pagedClients = clients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            //ViewBag.PageNumber = pageNumber;
            //ViewBag.PageSize = pageSize;
            //ViewBag.TotalPages = (int)Math.Ceiling((double)pagedClients.Count / pageSize);

            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(clients);
            var clientsViewModel = _mapper.Map<IEnumerable<ReadClientViewModel>>(clientsDto);

            return View(clientsViewModel);
        }

        public async Task<IActionResult> LoadDeleted()
        {
            var clients = await _clientServices.GetAllDeletedClients();

            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(clients);
            var clientsViewModel = _mapper.Map<IEnumerable<ReadClientViewModel>>(clientsDto);

            return View(clientsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchName, string searchPhone, string searchEmail)
        {
            var clients = await _clientServices.GetClientByMultipleParam(searchName, searchPhone, searchEmail);
            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(clients);
            var clientsViewModel = _mapper.Map<IEnumerable<ReadClientViewModel>>(clientsDto);
            return View(clientsViewModel);
        }

        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var client = await _clientServices.GetByIdAsyncServiceGeneric(id);
                if (client == null)
                {
                    return NotFound();
                }
                var vehicleList = await _unitOfWork.Vehicles.GetVehiclesByClientIdAsync(client.Id);

                var clientDto = _mapper.Map<ReadClientDto>(client);
                var clientViewModel = _mapper.Map<ReadClientViewModel>(clientDto);
                return View(clientViewModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel createClientViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createClientViewModel);
                }
                // Check if the email already exists
                var existingClient = await _clientServices.GetClientByEmail(createClientViewModel.Email);
                if (existingClient != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");

                    return View(createClientViewModel);
                }
                var clientDto = _mapper.Map<CreateClientDto>(createClientViewModel);
                var client = _mapper.Map<Client>(clientDto);
                var addedElement = await _clientServices.AddAsyncServiceGeneric(client);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientServices.GetByIdAsyncServiceGeneric(id);
            var clientDto = _mapper.Map<UpdateClientDto>(client);
            var clientView = _mapper.Map<UpdateClientViewModel>(clientDto);

            return View(clientView);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateClientViewModel updateClientViewModel)
        {
            try
            {
                var client = await _clientServices.GetByIdAsyncServiceGeneric(updateClientViewModel.Id);
                if (client == null)
                {
                    return NotFound();
                }
                var clientDto = _mapper.Map<UpdateClientDto>(updateClientViewModel);

                var updatedClient = _mapper.Map(clientDto, client);
                //updatedClient.UpdatedAt = DateTime.UtcNow;
                await _clientServices.UpdateAsyncServiceGeneric(updatedClient);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var client = await _clientServices.GetByIdAsyncServiceGeneric(id);
            if (client == null)
            {
                return NotFound();
            }
            var clientDto = _mapper.Map<DeleteClientDto>(client);
            var clientView = _mapper.Map<DeleteClientViewModel>(clientDto);
            return View(clientView);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteClientViewModel deleteClientViewModel)
        {
            var clientToDetete = await _clientServices.GetByIdAsyncServiceGeneric(deleteClientViewModel.Id);
            if (clientToDetete == null)
            {
                return NotFound();
            }
            clientToDetete.IsDeleted = true;
            clientToDetete.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                var deletedClients = await _clientServices.GetAllDeletedClients();
                var clientToRestore = deletedClients.Where(c => c.Id == id).FirstOrDefault();
                if (clientToRestore == null)
                {
                    return NotFound();
                }
                var existingClient = await _clientServices.GetByIdAsyncServiceGeneric(id);
                if (existingClient != null)
                {
                    await _clientServices.DetachAsyncServiceGeneric(existingClient);
                }
                var client = _mapper.Map<Client>(clientToRestore);
                client.IsDeleted = false;
                client.DeletedAt = null;
                await _clientServices.AttachAsyncServiceGeneric(client);
                await _unitOfWork.Clients.UpdateAsyncGeneric(client);

                var nb = await _unitOfWork.SaveChangesAsync();
                Console.WriteLine($"Number of changes saved: {nb}");
                return RedirectToAction(nameof(LoadDeleted));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error restoring client: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeletePermanently(int id)
        {
            try
            {
                var deletedClients = await _clientServices.GetAllDeletedClients();
                var clientToDelete = deletedClients.Where(c => c.Id == id).FirstOrDefault();
                if (clientToDelete == null)
                {
                    return NotFound();
                }
                var existingClient = await _clientServices.GetByIdAsyncServiceGeneric(id);
                if (existingClient != null)
                {
                    await _clientServices.DetachAsyncServiceGeneric(existingClient);
                }
                var client = _mapper.Map<Client>(clientToDelete);

                await _clientServices.AttachAsyncServiceGeneric(client);
                await _unitOfWork.Clients.DeleteAsyncGeneric(client);

                var nb = await _unitOfWork.SaveChangesAsync();
                Console.WriteLine($"Number of changes saved: {nb}");
                return RedirectToAction(nameof(LoadDeleted));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error restoring client: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }
    }
}