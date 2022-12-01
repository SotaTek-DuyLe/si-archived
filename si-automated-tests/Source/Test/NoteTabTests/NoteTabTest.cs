using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.NoteTabTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class NoteTabTest : BaseTest
    {
        [Category("NoteTab")]
        [Category("Huong")]
        [Test(Description = "Verify that notes tab loads correctly")]
        public void TC_232_Verify_that_notes_tab_loads_correctly()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/contacts");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password);
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            NoteTabPage noteTabPage = PageFactoryManager.Get<NoteTabPage>();
            noteTabPage.ClickOnElement(noteTabPage.NoteTab);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            string title = "Test Title1";
            string note = "test note1";
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/contractsites");
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            noteTabPage.ClickOnElement(noteTabPage.NoteTab);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/parties");
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickOnNotesTab();
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();
            
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/sites");
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            noteTabPage.ClickOnElement(noteTabPage.NoteTab);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();
            
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/weighbridgestations");
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            noteTabPage.ClickOnElement(noteTabPage.NoteTab);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();
            
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/weighbridgetickets");
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            noteTabPage.ClickOnElement(noteTabPage.NoteTab);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();
            
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/creditnotes");
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            noteTabPage.ClickOnElement(noteTabPage.NoteTab);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyElementVisibility(noteTabPage.TitleInput, true)
                .VerifyElementVisibility(noteTabPage.NoteTextArea, true);
            noteTabPage.SendKeys(noteTabPage.TitleInput, title);
            noteTabPage.SendKeys(noteTabPage.NoteTextArea, note);
            noteTabPage.ClickOnElement(noteTabPage.AddNoteButton);
            noteTabPage.WaitForLoadingIconToDisappear();
            noteTabPage.VerifyNewNote(title, note)
                .ClickCloseBtn()
                .SwitchToFirstWindow();
        }
    }
}
