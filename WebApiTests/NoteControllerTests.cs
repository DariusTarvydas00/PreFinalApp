// using System.Security.Claims;
// using App;
// using App.Dtos;
// using App.IServices;
// using DataAccess.Models;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using WepApi.Controllers;
//
// namespace WebApiTests;
//
// public class NoteControllerTests
// {
//     private readonly Mock<INoteService> _mockNoteService;
//     private readonly NoteController _controller;
//     private readonly List<Note> _notes;
//
//     public NoteControllerTests()
//     {
//         _mockNoteService = new Mock<INoteService>();
//         _controller = new NoteController(_mockNoteService.Object);
//         _notes = new List<Note>
//         {
//             new Note { Id = 1, Title = "Note 1", Text = "Content 1", UserId = 1, CategoryId = 1 },
//             new Note { Id = 2, Title = "Note 2", Text = "Content 2", UserId = 1, CategoryId = 2 }
//         };
//
//         // Setup the HttpContext to mimic User Identity and Claims
//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.NameIdentifier, "1")
//         };
//         var identity = new ClaimsIdentity(claims, "Test");
//         var claimsPrincipal = new ClaimsPrincipal(identity);
//         _controller.ControllerContext = new ControllerContext
//         {
//             HttpContext = new DefaultHttpContext { User = claimsPrincipal }
//         };
//     }
//
//     [Fact]
//     public void GetAll_ReturnsAllNotesForUser()
//     {
//         _mockNoteService.Setup(s => s.FindAll(1)).Returns(_notes);
//
//         var result = _controller.GetAll();
//
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedNotes = Assert.IsType<List<Note>>(okResult.Value);
//         Assert.Equal(2, returnedNotes.Count);
//     }
//
//     [Fact]
//     public void GetAllByContent_ReturnsFilteredNotes()
//     {
//         _mockNoteService.Setup(s => s.FindAllByContent("Content", 1)).Returns(_notes);
//
//         var result = _controller.GetAllByContent("Content");
//
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedNotes = Assert.IsType<List<Note>>(okResult.Value);
//         Assert.Equal(2, returnedNotes.Count);
//     }
//
//     [Fact]
//     public void GetById_ReturnsNote_WhenNoteExists()
//     {
//         _mockNoteService.Setup(s => s.GetById(1, 1)).Returns(_notes[0]);
//
//         var result = _controller.GetById(1);
//
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedNote = Assert.IsType<Note>(okResult.Value);
//         Assert.Equal("Note 1", returnedNote.Title);
//     }
//
//     [Fact]
//     public void Create_ReturnsCreatedNote_WhenModelIsValid()
//     {
//         var newNote = new Note { Title = "New Note", Text = "New content", CategoryId = 1, UserId = 1 };
//         _mockNoteService.Setup(s => s.Create(It.IsAny<Note>())).Returns(newNote);
//
//         var result = _controller.Create(new NoteCreateDto { Title = "New Note", Text = "New content", CategoryId = 1 });
//
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedNote = Assert.IsType<Note>(okResult.Value);
//         Assert.Equal("New Note", returnedNote.Title);
//     }
//
//     [Fact]
//     public void Update_ReturnsUpdatedNote_WhenModelIsValid()
//     {
//         var updatedNote = new Note { Id = 1, Title = "Updated Note", Text = "Updated content", CategoryId = 1, UserId = 1 };
//         _mockNoteService.Setup(s => s.Update(It.IsAny<Note>())).Returns(updatedNote);
//
//         var result = _controller.Update(new NoteUpdateDto { Id = 1, Title = "Updated Note", Text = "Updated content", CategoryId = 1 });
//
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedNote = Assert.IsType<Note>(okResult.Value);
//         Assert.Equal("Updated Note", returnedNote.Title);
//     }
//
//     [Fact]
//     public void Delete_ReturnsDeletedNote_WhenNoteExists()
//     {
//         _mockNoteService.Setup(s => s.Delete(1, 1)).Returns(_notes[0]);
//
//         var result = _controller.Delete(1);
//
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedNote = Assert.IsType<Note>(okResult.Value);
//         Assert.Equal(1, returnedNote.Id);
//     }
// }