//
// using App.Services;
// using DataAccess.IRepositories;
// using DataAccess.Models;
// using Moq;
//
// namespace AppTests;
//
// public class NoteServiceTests
// {
//     private readonly Mock<INoteRepository> _mockRepo;
//     private readonly NoteService _noteService;
//
//     public NoteServiceTests()
//     {
//         _mockRepo = new Mock<INoteRepository>();
//         _noteService = new NoteService(_mockRepo.Object);
//     }
//
//     [Fact]
//     public void FindAll_ReturnsAllNotesForUser()
//     {
//         var notes = new List<Note> { new Note(), new Note() };
//         _mockRepo.Setup(repo => repo.FindAll(1)).Returns(notes);
//
//         var result = _noteService.FindAll(1);
//
//         Assert.Equal(notes, result);
//     }
//
//     [Fact]
//     public void FindAllByContent_ReturnsFilteredNotes()
//     {
//         var notes = new List<Note> { new Note { Text = "test" } };
//         _mockRepo.Setup(repo => repo.FindAllByContent("test", 1)).Returns(notes);
//
//         var result = _noteService.FindAllByContent("test", 1);
//
//         Assert.Equal(notes, result);
//     }
//
//     [Fact]
//     public void GetById_ReturnsNote_WhenNoteExists()
//     {
//         var note = new Note();
//         _mockRepo.Setup(repo => repo.GetById(1, 1)).Returns(note);
//
//         var result = _noteService.GetById(1, 1);
//
//         Assert.Equal(note, result);
//     }
//
//     [Fact]
//     public void GetById_ReturnsNull_WhenNoteDoesNotExist()
//     {
//         _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>(), It.IsAny<int>())).Returns((Note)null);
//
//         var result = _noteService.GetById(999, 1);
//
//         Assert.Null(result);
//     }
//
//     [Fact]
//     public void Create_ThrowsArgumentNullException_WhenNoteIsNull()
//     {
//         Assert.Throws<ArgumentNullException>(() => _noteService.Create(null));
//     }
//
//     [Fact]
//     public void Create_ReturnsNote_WhenNoteIsValid()
//     {
//         var note = new Note();
//         _mockRepo.Setup(repo => repo.Create(note)).Returns(note);
//
//         var result = _noteService.Create(note);
//
//         Assert.Equal(note, result);
//     }
//
//     [Fact]
//     public void Update_ThrowsArgumentNullException_WhenNoteIsNull()
//     {
//         Assert.Throws<ArgumentNullException>(() => _noteService.Update(null));
//     }
//
//     [Fact]
//     public void Update_ReturnsUpdatedNote_WhenNoteIsValid()
//     {
//         var note = new Note();
//         _mockRepo.Setup(repo => repo.Update(note)).Returns(note);
//
//         var result = _noteService.Update(note);
//
//         Assert.Equal(note, result);
//     }
//
//     [Fact]
//     public void Delete_ReturnsDeletedNote()
//     {
//         var note = new Note();
//         _mockRepo.Setup(repo => repo.Delete(1, 1)).Returns(note);
//
//         var result = _noteService.Delete(1, 1);
//
//         Assert.Equal(note, result);
//     }
// }