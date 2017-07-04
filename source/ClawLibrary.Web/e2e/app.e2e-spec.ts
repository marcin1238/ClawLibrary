import { ClawLibraryPage } from './app.po';

describe('claw-library App', () => {
  let page: ClawLibraryPage;

  beforeEach(() => {
    page = new ClawLibraryPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
