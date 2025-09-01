using ClosedXML.Excel;
using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Service.Implementation
{
    public class BookMarkFolderService : IBookMarkFolderService
    {
        private readonly IRepository<BookmarkFolder> repository;
        private readonly IUserRepository userRepository;

        public BookMarkFolderService(IRepository<BookmarkFolder> repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public BookmarkFolder Add(BookmarkFolder folder)
        {
            return repository.Insert(folder);
        }

        public BookmarkFolder Delete(Guid id)
        {
            var folder = GetById(id);
            if (folder == null)
            {
                throw new ArgumentException("Folder not found", nameof(id));
            }

            return repository.Delete(folder);
        }

        public byte[] ExportFileToCSV(Guid folderId)
        {

            var folder = GetByIdFullData(folderId);

            if (folder == null) throw new Exception($"Folder to be exported with id ${folderId} cannot be found");

            using (var workbook = new XLWorkbook()) {

                var worksheet = workbook.Worksheets.Add(folder.Name ?? "Folder1");

                // Info Headers and Data
                worksheet.Cell(2, 4).Value = "Folder Name     ";
                worksheet.Cell(2, 5).Value = "User  ";
                var headerRange1 = worksheet.Range(2, 4, 2, 5);
                worksheet.Cell(3, 4).Value = folder.Name;
                worksheet.Cell(3, 5).Value = folder.User!.UserName;


                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images","logo.png");

                Console.WriteLine(imagePath);

                var image = worksheet.AddPicture(imagePath) 
                     .MoveTo(worksheet.Cell(1, 1))  
                     .Scale(0.2); 

                // Add title next to it
                worksheet.Cell(1, 1).Value = "Live News App           ";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;


                // Table headers
                var headerRow = 7;
                worksheet.Cell(headerRow, 1).Value = "Title";
                worksheet.Cell(headerRow, 2).Value = "Author";
                worksheet.Cell(headerRow, 3).Value = "Source";
                worksheet.Cell(headerRow, 4).Value = "Language";
                worksheet.Cell(headerRow, 5).Value = "Country";
                worksheet.Cell(headerRow, 6).Value = "Category";
                worksheet.Cell(headerRow, 7).Value = "Published";
                worksheet.Cell(headerRow, 8).Value = "Link";

                var headerRange2 = worksheet.Range(headerRow, 1, headerRow, 8);
                headerRange1.Style.Font.Bold = true;
                headerRange2.Style.Font.Bold = true;

                headerRange1.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange2.Style.Fill.BackgroundColor = XLColor.LightGray;

                int row = 8;
                foreach (var articleInFolder in folder.ArticleInFolders!)
                {
                    var article = articleInFolder.NewsArticle;

                    if (article == null) throw new Exception($"Article is null for ArticleInFolderId: ${articleInFolder.Id} and folder name {{{folder.Name}}} and id {{{folder.Id}}}");
                    
                    //if (article.Language == null) throw new Exception($"Article's Language is null for ArticleInFolderId: ${articleInFolder.Id} and folder name {{{folder.Name}}} and id {{{folder.Id}}} and Article with id {{{article.Id}}}");
                    //if (article.Country == null) throw new Exception($"Article's Country is null for ArticleInFolderId: ${articleInFolder.Id} and folder name {{{folder.Name}}} and id {{{folder.Id}}} and Article with id {{{article.Id}}}");
                    //if (article.Category == null) throw new Exception($"Article's Category is null for ArticleInFolderId: ${articleInFolder.Id} and folder name {{{folder.Name}}} and id {{{folder.Id}}} and Article with id {{{article.Id}}}");

                    worksheet.Cell(row, 1).Value = article.Title;
                    worksheet.Cell(row, 2).Value = article.Author;
                    worksheet.Cell(row, 3).Value = article.Source;
                    worksheet.Cell(row, 4).Value = article.Language!=null ? article.Language.Name : "Blank";
                    worksheet.Cell(row, 5).Value = article.Country!= null ? article.Country.Name : "Blank";
                    worksheet.Cell(row, 6).Value = article.Category!= null ? article.Category.Code : "Blank";
                    worksheet.Cell(row, 7).Value = article.PublishedAt;

                    // Make the link clickable
                    worksheet.Cell(row, 8).Value = "Link";
                    worksheet.Cell(row, 8).SetHyperlink(new XLHyperlink(article.Url));

                    row++;
                }


                worksheet.Columns().AdjustToContents();
                worksheet.Column(1).Width = 45;

                var contentRange = worksheet.Range(8, 1, row, 8);
                contentRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                contentRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                contentRange.Style.Alignment.WrapText = true;

                worksheet.Row(1).Height = 36;
                worksheet.Row(3).Height = 22;


                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.FitToPages(1, 0); // Fit all columns on 1 page wide, unlimited rows
                worksheet.PageSetup.SetRowsToRepeatAtTop(headerRow, headerRow); // Assuming row 5 is your table header

                worksheet.PageSetup.Margins.Bottom = 1.0; // inches, increase if needed


                worksheet.PageSetup.Footer.Left.AddText($"Live News App {DateTime.Today.Year} - Ljupce Angelovski");
                worksheet.PageSetup.Footer.Center.AddText("&P/&N"); // page number / total pages
                worksheet.PageSetup.Footer.Right.AddText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}");

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }

        }

        public IEnumerable<BookmarkFolder> GetAll()
        {
            return repository.GetAll(selector: f => f,
                include: fi => fi
                 .Include(i => i.User!)
                 .Include(i => i.ArticleInFolders!)
                 .ThenInclude(af => af.NewsArticle!));
        }

        public BookmarkFolder? GetById(Guid id)
        {
            return repository.Get(selector: f => f,
                predicate: fp => fp.Id == id,
                include: fi => fi
                 .Include(i => i.User!)
                 .Include(i => i.ArticleInFolders!)
                 .ThenInclude(af => af.NewsArticle!));
        }

        public BookmarkFolder? GetByIdFullData(Guid id)
        {
            return repository.Get(selector: f => f,
                predicate: fp => fp.Id == id,
                include: fi => fi
                 .Include(i => i.User!)
                 .Include(i => i.ArticleInFolders!)
                    .ThenInclude(af => af.NewsArticle!)
                        .ThenInclude(na => na.Category!)
                .Include(i => i.ArticleInFolders!)
                    .ThenInclude(af => af.NewsArticle!)
                        .ThenInclude(na => na.Language!)
                .Include(i => i.ArticleInFolders!)
                    .ThenInclude(af => af.NewsArticle!)
                        .ThenInclude(na => na.Country!));
        }

        public BookmarkFolder? GetByIdNoData(Guid id)
        {
            return repository.Get(selector: f => f,
                predicate: fp => fp.Id == id);
        }

        public IEnumerable<BookmarkFolder> GetFolderNamesByUserId(string? userId)
        {
            if (userId == null)
            {
                throw new Exception("UserId is null");

            }

            return repository.GetAll(selector: f => f,
                predicate: fp => fp.UserId == userId);
        }

        public IEnumerable<BookmarkFolder> GetFoldersByUserId(string? userId)
        {
            if (userId == null)
            {
                throw new Exception("UserId is null");

            }

            return repository.GetAll(selector: f => f,
                predicate: fp => fp.UserId == userId,
                include: fi => fi
                 .Include(i => i.User!)
                 .Include(i => i.ArticleInFolders!)
                 .ThenInclude(af => af.NewsArticle!));
        }



        public BookmarkFolder Update(BookmarkFolder folder)
        {
            return repository.Update(folder);
        }
    }
}
