using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

namespace CST.Models
{
    public class SyncfusionPDFBook
    {
        public void AddBookmark(PdfPage page, PdfPage toc, string content, PointF point, PdfDocument document, PdfFont font)
        {
            PdfGraphics graphics = page.Graphics;
            //Add bookmark in PDF document
            PdfBookmark bookmarks = document.Bookmarks.Add(content);
            //Add table of contents
            AddTableOfContent(page, toc, content, point, document, font);
            //Adding bookmark with named destination
            PdfNamedDestination namedDestination = new PdfNamedDestination(content);
            namedDestination.Destination = new PdfDestination(page, new PointF(point.X, point.Y));
            namedDestination.Destination.Mode = PdfDestinationMode.FitToPage;
            document.NamedDestinationCollection.Add(namedDestination);
            bookmarks.NamedDestination = namedDestination;
        }

        //Add table of content with page number and document link annotations
        private void AddTableOfContent(PdfPage page, PdfPage toc, string content, PointF point, PdfDocument document, PdfFont font)
        {
            //Draw title in TOC
            PdfTextElement element = new PdfTextElement(content, font);
            //Set layout format for pagination of TOC
            PdfLayoutFormat format = new PdfLayoutFormat();
            format.Break = PdfLayoutBreakType.FitPage;
            format.Layout = PdfLayoutType.Paginate;
            PdfLayoutResult result = element.Draw(toc, point, format);
            //Draw page number in TOC
            PdfTextElement pageNumber = new PdfTextElement(document.Pages.IndexOf(page).ToString(), font, PdfBrushes.Black);
            pageNumber.Draw(toc, new PointF(toc.Graphics.ClientSize.Width - 40, point.Y));
            //Creates a new document link annotation
            RectangleF bounds = result.Bounds;
            bounds.Width = toc.Graphics.ClientSize.Width - point.X;
            PdfDocumentLinkAnnotation documentLinkAnnotation = new PdfDocumentLinkAnnotation(bounds);
            documentLinkAnnotation.AnnotationFlags = PdfAnnotationFlags.NoRotate;
            documentLinkAnnotation.Text = content;
            documentLinkAnnotation.Color = Color.Transparent;
            //Sets the destination
            documentLinkAnnotation.Destination = new PdfDestination(page);
            documentLinkAnnotation.Destination.Location = point;
            //Adds this annotation to a new page
            toc.Annotations.Add(documentLinkAnnotation);
        }
    }
}
