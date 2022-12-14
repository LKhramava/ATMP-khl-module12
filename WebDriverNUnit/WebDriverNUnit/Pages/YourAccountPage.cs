using OpenQA.Selenium;
using System;
using System.Threading;
using WebDriverNUnit.Entities;
using WebDriverNUnit.WebDriver;

namespace WebDriverNUnit.Pages
{
	public class YourAccountPage : BasePage
	{
		private static readonly By AccountLbl = By.Id("app-canvas");

		public YourAccountPage() : base(AccountLbl, "Account Page")
		{
		}

		private readonly BaseElement sideBarContentBE = new BaseElement(By.Id("sideBarContent"));

		private readonly BaseElement newEmailBE = new BaseElement(By.XPath("//a[contains(@href,'compose')]"));
		private readonly BaseElement newEmailWindowBE = new BaseElement(By.CssSelector(".compose-app__compose"));
		private readonly BaseElement newEmailWindowCloseBE = new BaseElement(By.XPath("//button[@data-promo-id='extend']/following-sibling::button"));

		private readonly BaseElement toBE = new BaseElement(By.XPath("//div[contains(@data-type,'to')]//input"));
		private readonly BaseElement subjectBE = new BaseElement(By.XPath("//input[contains(@name,'Subject')]"));
		private readonly BaseElement bodyBE = new BaseElement(By.XPath("(//div[@contenteditable='true']//div)[1]"));

		private readonly BaseElement saveDraftBE = new BaseElement(By.XPath("//button[@data-test-id='save']"));

		private readonly BaseElement mainContainerBE = new BaseElement(AccountLbl);

		private By lettersBy = By.XPath("//div[contains(@class, 'letter-list')]//a[contains(@class, 'js-letter-list-item')]");
		private By letterCorrespondentBy = By.XPath("//span[contains(@class, 'll-crpt')]");
		private By letterSubjectBy = By.XPath("//span[contains(@class, 'llc__subject')]//span");
		private By letterSnippetBy = By.XPath("//span[contains(@class, 'llc__snippet')]//span");

		private readonly BaseElement draftBE = new BaseElement(By.XPath("//div[@id='sideBarContent']//a[contains(@href,'drafts')]"));

		private readonly BaseElement sentBE = new BaseElement(By.XPath("//div[@id='sideBarContent']//a[contains(@href,'sent')]"));

		private readonly BaseElement trashBE = new BaseElement(By.XPath("//div[@id='sideBarContent']//a[contains(@href,'trash')]"));

		private readonly BaseElement composeAppPopupBE = new BaseElement(By.XPath("//div[contains(@class, 'compose-app_popup')]"));
		private readonly BaseElement composeAppPopupSendBE = new BaseElement(By.XPath("//div[contains(@class, 'compose-app_popup')]//button[@data-test-id='send']"));
		private readonly BaseElement sentMessageCloseBE = new BaseElement(By.CssSelector(".layer__controls"));

		private readonly BaseElement accoutContainerBE = new BaseElement(By.XPath("//div[contains(@class, 'ph-project__account')]"));
		private readonly BaseElement exitBE = new BaseElement(By.XPath("//div[contains(@class, 'ph-accounts')]//div[contains(@class, 'ph-item')]//div[contains(@class, 'ph-icon')]"));

		private static string jsLetterListItemString = "//a[contains(@class, 'js-letter-list-item') and contains(@href, '{0}')]";
		private readonly BaseElement draftsItemsBE = new BaseElement(By.XPath(string.Format(jsLetterListItemString, "drafts")));
		private readonly BaseElement sentItemsBE = new BaseElement(By.XPath(string.Format(jsLetterListItemString, "sent")));
		private readonly BaseElement trashItemsBE = new BaseElement(By.XPath(string.Format(jsLetterListItemString, "trash")));

		private static string jsLetterListItemDraftString = string.Format(jsLetterListItemString, "drafts");
		private readonly BaseElement firstDraftItemBE = new BaseElement(By.XPath("(" + jsLetterListItemDraftString + ")[1]"));
		private readonly BaseElement firstDraftItemBackgroundBE = new BaseElement(By.XPath("(" + jsLetterListItemDraftString + ")[1]//div[contains(@class, 'llc__background')]"));

		private static string loginString = "//span[contains(@class, 'ph-project__user-name') and text()='{0}']";

		public void SaveDraftEmail(Letter letter)
		{
			SaveDraftEmailInternal(letter);
		}

		public bool VerifySavedDraftEmail(Letter letter)
		{
			//	Verify, that the mail presents in ‘Drafts’ folder
			//	Verify the draft content (addressee, subject and body – should be the same as in 3).

			//click on Draft folder and make sure that we have moved to the page with the draft elements
			draftBE.Click();
			draftBE.UrlContainsFraction("drafts");
			draftsItemsBE.WaitForIsVisible();

			BaseElement draftSubjectBE = new BaseElement(By.XPath("//span[contains(@class, 'll-sj__normal') and text()='" + letter.Subject + "']"));
			draftSubjectBE.WaitForIsVisible();

			var letterInDraft = FindLetterInList(lettersBy, letter);
			return letterInDraft != null;
		}

		public bool SendDraftEmail(Letter letter)
		{
			return SendDraftEmailInternal(letter);
		}

		public bool DelteFirstDraftEmail()
		{
			//use JS Executor

			//click on Draft folder and make sure that we have moved to the page with the draft elements
			draftBE.JSClick();
			draftBE.UrlContainsFraction("drafts");
			draftsItemsBE.WaitForIsVisible();

			//find first letter in the list
			var firstDraftWebElement = firstDraftItemBE.GetElement();
			var email = firstDraftWebElement.FindElement(letterCorrespondentBy).GetAttribute("title");
			var subject = firstDraftWebElement.FindElement(letterSubjectBy).Text;
			var data = firstDraftWebElement.FindElement(letterSnippetBy).Text;

			//hightlight first letter
			firstDraftItemBackgroundBE.JSHighlight();
			firstDraftItemBE.MoveToElement();

			//move the first letter to the deleted folder(trash)
			firstDraftItemBE.DragAndDropWithActions(trashBE.GetElement());

			//click on Trash folder and make sure that we have moved to the page with the trash elements
			trashBE.Click();
			trashBE.UrlContainsFraction("trash");
			trashItemsBE.WaitForIsVisible();

			//find letter in trash folder
			var letter = new Letter()
			{
				Email = email,
				Subject = subject,
				Body = data
			};
			var letterInDraft = FindLetterInList(lettersBy, letter);
			return letterInDraft != null;
		}

		public void Logout()
		{
			accoutContainerBE.Click();
			exitBE.Click();
		}

		private void SaveDraftEmailInternal(Letter letter)
		{
			newEmailBE.Click();
			newEmailWindowBE.WaitForIsVisible();

			toBE.SendKeys(letter.Email);
			subjectBE.SendKeys(letter.Subject);
			bodyBE.SendKeys(letter.Body);

			//	Save the mail as a draft.
			saveDraftBE.Click();

			var newEmailWindowClose = newEmailWindowBE.FindElement(newEmailWindowCloseBE);
			newEmailWindowClose.Click();
		}

		private bool SendDraftEmailInternal(Letter letter)
		{
			//	Verify, that the mail presents in ‘Drafts’ folder
			//	Verify the draft content (addressee, subject and body – should be the same as in 3).
			draftBE.Click();
			draftBE.UrlContainsFraction("drafts");
			draftsItemsBE.WaitForIsVisible();

			var letterInDraft = FindLetterInList(lettersBy, letter);
			if (letterInDraft != null)
			{
				letterInDraft.Click();
				composeAppPopupBE.WaitForIsVisible();

				//	Send the mail
				composeAppPopupSendBE.Click();
				sentMessageCloseBE.Click();

				var checkLetterInDraftResult = CheckLetterInDraft(letter);
				var checkLetterInSentResult = CheckLetterInSent(letter);
				return checkLetterInDraftResult == null && checkLetterInSentResult != null;
			}
			return false;
		}

		private IWebElement? CheckLetterInDraft(Letter letter)
		{
			//	Verify, that the mail disappeared from ‘Drafts’ folder
			sideBarContentBE.Click();
			draftBE.Click();

			draftBE.UrlContainsFraction("drafts");
			draftsItemsBE.WaitForIsVisible();
			var notExistLetterInDraft = FindLetterInList(lettersBy, letter);
			return notExistLetterInDraft;
		}

		private IWebElement? CheckLetterInSent(Letter letter)
		{
			//	Verify, that the mail is in ‘Sent’ folder.
			sideBarContentBE.Click();
			sentBE.Click();

			draftBE.UrlContainsFraction("sent");
			sentItemsBE.WaitForIsVisible();
			var letterInSent = FindLetterInList(lettersBy, letter);
			return letterInSent;
		}

		private IWebElement FindLetterInList(By letters, Letter letter)
		{
			var lettersList = Browser.GetDriver().FindElements(letters);

			foreach (var letterListItem in lettersList)
			{
				var email = letterListItem.FindElement(letterCorrespondentBy).GetAttribute("title");
				var subject = letterListItem.FindElement(letterSubjectBy).Text;
				var data = letterListItem.FindElement(letterSnippetBy).Text;

				if (email.Contains(letter.Email, StringComparison.OrdinalIgnoreCase) &&
					subject.Contains(letter.Subject, StringComparison.OrdinalIgnoreCase) &&
					data.Contains(letter.Body, StringComparison.OrdinalIgnoreCase))
				{
					return letterListItem;
				}
			}
			return null;
		}

		public bool IsLoginDisplayed(string login)
		{
			var loginStringBE = new BaseElement(By.XPath(string.Format(loginString, login)));
			return loginStringBE.IsDisplayed();
		}
	}
}
