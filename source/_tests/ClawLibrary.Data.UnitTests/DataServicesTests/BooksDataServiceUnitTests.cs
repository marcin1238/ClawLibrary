using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Data.DataServices;
using ClawLibrary.Data.Mapping;
using ClawLibrary.Data.Models;
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace ClawLibrary.Data.UnitTests.DataServicesTests
{
    [TestFixture]
    public class BooksDataServiceUnitTests
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private List<Book> _data;

        public BooksDataServiceUnitTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataMappingProfile());
            }));
        }

        [SetUp]
        protected void SetUp()
        {
            _data = new List<Book>()
            {
                new Book() { Id  = 1, Key  = new Guid("6A0734F0-B3DE-44D6-93E3-F9E504A35A6E"), Title  = "Licensed Tasty Wooden Bike 78", Publisher  = "Fay Group", Language  = "PL", Isbn  = "40446591",Description  = "Quam voluptates modi labore aut non sit officiis laborum quo reiciendis nulla veritatis numquam blanditiis est unde voluptate fugit quibusdam id et optio voluptatum porro nesciunt officia assumenda accusantium qui alias quidem placeat et iste veniam ad labore maiores debitis tenetur veniam fuga sit aliquam delectus ipsum adipisci mollitia consequatur sint minima nihil qui dignissimos dolor non placeat cupiditate vel nisi dolore nulla aut quo nisi explicabo nesciunt expedita quia autem nemo quam labore voluptas qui et consequatur modi placeat doloribus reprehenderit quia eius consequatur quisquam numquam est laudantium vel quasi non quo quia provident nihil odio quod dolores voluptates officiis perferendis sed nihil et est eos amet ullam doloribus natus nulla cupiditate omnis dolorem hic architecto et architecto sed consequuntur corrupti eligendi minima aut pariatur mollitia dolore nihil dolores perferendis debitis officiis enim debitis aut at facere repellendus velit eos doloremque nemo rem illum tempora alias itaque ut qui harum consequatur libero autem repellendus eum voluptatem quaerat voluptatem qui perferendis laborum atque dolorem reiciendis nobis aut numquam ut at aspernatur voluptatem nulla debitis quaerat aspernatur sit modi praesentium debitis nulla expedita eveniet sed quas sed non dolores non quos et mollitia et velit sit dolore unde qui dolores dolores aliquam temporibus eaque dolores corrupti enim delectus autem sed nobis sed aspernatur et dignissimos id ex porro commodi nam est voluptatum deleniti nobis voluptate velit et necessitatibus facilis eligendi aut omnis ipsa qui quia dolorum inventore dolorum in harum consectetur labore quam autem voluptas quis iste quibusdam maiores laudantium dolorem provident amet deleniti quisquam distinctio omnis.", Quantity  = 97, Paperback  = 409, PublishDate  = new DateTime(1929,12,22), AuthorId  = 2,ImageFileId  = null, CategoryId  = 23, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,9,12)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 2, Key = new Guid("66C50043-BD59-4B74-B8C3-00EF22712324"), FirstName = "Floy", LastName = "Stroman",Description = "Sunt optio unde expedita omnis qui et sunt modi omnis eveniet velit tempore quis at quia optio et ullam magni non ad quasi praesentium voluptate iure explicabo voluptatum temporibus officiis commodi voluptas corporis recusandae voluptatum nam ratione mollitia eum est debitis doloribus et sit neque nisi recusandae qui dicta aut qui aut repellat quia numquam vel et sunt dolore neque corporis voluptatem corporis aliquid velit magni odit quod vel amet reiciendis aspernatur aperiam cupiditate aliquam earum aperiam ab exercitationem et blanditiis commodi reprehenderit omnis qui deleniti assumenda sit quisquam distinctio eaque inventore aliquid libero rem est est saepe id doloremque labore adipisci sint eveniet aperiam ipsa pariatur rem accusamus ad omnis in autem sit impedit itaque omnis cupiditate dolorum non commodi molestiae sit sequi voluptatem et placeat consequatur illo non aliquam qui dignissimos ut eum voluptatem ducimus quos maxime est harum exercitationem enim ducimus aut praesentium voluptatem dolorem a provident delectus neque enim cum libero enim quia omnis placeat fugit nostrum deleniti quam nisi voluptatem quidem dolores non ex dignissimos quos enim dolor non ullam et et sunt enim eligendi expedita qui voluptate occaecati error sunt accusamus assumenda animi sint dicta nobis maxime et qui sequi debitis ut molestiae dolore quia id vel sint ipsa optio eum nisi autem animi optio id quos illo blanditiis impedit quasi eveniet omnis non voluptas commodi non quo ex aut cupiditate voluptatem provident qui consectetur facilis ratione accusantium consequatur et assumenda saepe deleniti accusamus impedit non reprehenderit repellat qui atque adipisci est ipsum est vitae reiciendis consectetur dolores deleniti sapiente.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 23, Key = new Guid("4D8C4A98-8B78-417B-BDCE-84799C2D875B"), Name = "Cookbooks", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 2, Key  = new Guid("7FB56B07-18E8-4F01-9FB8-5A474A59ADCC"), Title  = "Generic Incredible Frozen Table 136", Publisher  = "Boyle Inc", Language  = "PL", Isbn  = "13486737",Description  = "Rem sed inventore aut voluptas sed facilis nihil ut sit et deleniti consequatur error consequatur nam nulla tenetur doloribus aut est assumenda sint voluptatem ut impedit consequuntur sit voluptatem neque repellendus sunt numquam blanditiis consequatur sit id tempore quaerat excepturi expedita minima occaecati eos ut fuga assumenda animi commodi consequatur reiciendis est maxime non beatae nihil dolorum id fugiat dolores asperiores praesentium eum id in omnis ut voluptates iste qui inventore ipsum optio deleniti ducimus quis dolore omnis dicta rerum earum non illum molestias amet veritatis ut alias vel ullam error et quo consequuntur occaecati velit saepe repellendus quod praesentium ea vero ipsa perspiciatis placeat aut accusantium voluptatem voluptas impedit praesentium nulla et consectetur adipisci laudantium rerum officiis omnis laboriosam molestiae voluptatem provident aliquam quaerat adipisci suscipit similique sapiente magnam voluptatum laudantium aperiam et veniam vitae atque ad omnis vel aperiam vitae ut libero voluptas quisquam non facilis recusandae illo iste sit incidunt ea doloremque explicabo voluptas repellendus quis iste rem blanditiis corporis sint incidunt repellat consequatur accusantium voluptates aut voluptatibus commodi fugit ipsum corporis ut accusantium eos esse sed voluptatibus est quidem voluptates eligendi praesentium incidunt et animi amet facere optio doloribus sed voluptates in omnis quisquam iusto nulla voluptates amet id qui architecto consectetur ut veritatis aspernatur soluta quasi totam quis placeat sunt repellendus qui qui qui est voluptas dolor molestiae est ea veniam autem repudiandae voluptatem qui similique nemo nihil similique et autem tenetur illum fugiat et iste ipsum provident sint sed quis voluptas labore et consequuntur facere eum eveniet nam beatae qui.", Quantity  = 96, Paperback  = 557, PublishDate  = new DateTime(1993,11,1), AuthorId  = 2,ImageFileId  = null, CategoryId  = 2, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,10)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 2, Key = new Guid("66C50043-BD59-4B74-B8C3-00EF22712324"), FirstName = "Floy", LastName = "Stroman",Description = "Sunt optio unde expedita omnis qui et sunt modi omnis eveniet velit tempore quis at quia optio et ullam magni non ad quasi praesentium voluptate iure explicabo voluptatum temporibus officiis commodi voluptas corporis recusandae voluptatum nam ratione mollitia eum est debitis doloribus et sit neque nisi recusandae qui dicta aut qui aut repellat quia numquam vel et sunt dolore neque corporis voluptatem corporis aliquid velit magni odit quod vel amet reiciendis aspernatur aperiam cupiditate aliquam earum aperiam ab exercitationem et blanditiis commodi reprehenderit omnis qui deleniti assumenda sit quisquam distinctio eaque inventore aliquid libero rem est est saepe id doloremque labore adipisci sint eveniet aperiam ipsa pariatur rem accusamus ad omnis in autem sit impedit itaque omnis cupiditate dolorum non commodi molestiae sit sequi voluptatem et placeat consequatur illo non aliquam qui dignissimos ut eum voluptatem ducimus quos maxime est harum exercitationem enim ducimus aut praesentium voluptatem dolorem a provident delectus neque enim cum libero enim quia omnis placeat fugit nostrum deleniti quam nisi voluptatem quidem dolores non ex dignissimos quos enim dolor non ullam et et sunt enim eligendi expedita qui voluptate occaecati error sunt accusamus assumenda animi sint dicta nobis maxime et qui sequi debitis ut molestiae dolore quia id vel sint ipsa optio eum nisi autem animi optio id quos illo blanditiis impedit quasi eveniet omnis non voluptas commodi non quo ex aut cupiditate voluptatem provident qui consectetur facilis ratione accusantium consequatur et assumenda saepe deleniti accusamus impedit non reprehenderit repellat qui atque adipisci est ipsum est vitae reiciendis consectetur dolores deleniti sapiente.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 2, Key = new Guid("4D4393B4-A6F6-40AB-A18D-96703462496E"), Name = "Satire", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 3, Key  = new Guid("8BEB61E6-50CA-417E-9784-690E32B905F6"), Title  = "Small Practical Soft Bacon 479", Publisher  = "Schmidt, Hills and Brown", Language  = "DE", Isbn  = "08178053",Description  = "Numquam mollitia est molestias id architecto tenetur eos veniam dolores aut sit voluptate omnis quos eveniet aperiam earum ex quae nobis similique consequatur neque aut ex et non fugit pariatur exercitationem placeat dolorem in ducimus atque vitae aliquam voluptate iure quo soluta impedit nam qui molestiae ex veniam enim tempora maxime necessitatibus est sint quidem est tempora corrupti cumque magnam eum molestiae voluptatem et voluptas tempora ea fugiat enim aut vel dolor provident delectus quia consequatur eos nihil assumenda debitis alias sapiente eius necessitatibus odit ea aspernatur enim itaque et aliquam mollitia omnis nesciunt iste voluptate expedita ad dolorem dolorem similique occaecati aut ullam velit delectus ea nobis nemo harum temporibus cupiditate porro qui magnam accusamus non dolores quia iusto consequuntur quae eveniet excepturi facere eius tempora autem voluptate quibusdam ut qui debitis animi enim deleniti maiores sint voluptatum qui quae esse alias corporis aut qui debitis minus et est velit cupiditate hic reprehenderit ipsa occaecati dolorum praesentium perspiciatis adipisci id voluptatibus ad molestias quis quia facilis sed excepturi assumenda aut nostrum expedita sit iure impedit ipsa mollitia et accusantium laboriosam dolores totam quisquam mollitia qui harum corrupti iste temporibus vero non quis minima ea et qui dolores sit aut dicta possimus eius eos aut voluptates et assumenda alias sit voluptatum accusantium sapiente occaecati at et excepturi omnis voluptates laudantium sunt sequi error est voluptates ex et vel dolor quia similique unde in eligendi earum qui ad sit sint est tenetur et corrupti modi enim fugiat error pariatur molestiae neque omnis officia qui iste incidunt non.", Quantity  = 82, Paperback  = 857, PublishDate  = new DateTime(1938,11,25), AuthorId  = 2,ImageFileId  = null, CategoryId  = 28, CreatedDate  = new DateTimeOffset(new DateTime(2017,10,12)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,10,12)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 2, Key = new Guid("66C50043-BD59-4B74-B8C3-00EF22712324"), FirstName = "Floy", LastName = "Stroman",Description = "Sunt optio unde expedita omnis qui et sunt modi omnis eveniet velit tempore quis at quia optio et ullam magni non ad quasi praesentium voluptate iure explicabo voluptatum temporibus officiis commodi voluptas corporis recusandae voluptatum nam ratione mollitia eum est debitis doloribus et sit neque nisi recusandae qui dicta aut qui aut repellat quia numquam vel et sunt dolore neque corporis voluptatem corporis aliquid velit magni odit quod vel amet reiciendis aspernatur aperiam cupiditate aliquam earum aperiam ab exercitationem et blanditiis commodi reprehenderit omnis qui deleniti assumenda sit quisquam distinctio eaque inventore aliquid libero rem est est saepe id doloremque labore adipisci sint eveniet aperiam ipsa pariatur rem accusamus ad omnis in autem sit impedit itaque omnis cupiditate dolorum non commodi molestiae sit sequi voluptatem et placeat consequatur illo non aliquam qui dignissimos ut eum voluptatem ducimus quos maxime est harum exercitationem enim ducimus aut praesentium voluptatem dolorem a provident delectus neque enim cum libero enim quia omnis placeat fugit nostrum deleniti quam nisi voluptatem quidem dolores non ex dignissimos quos enim dolor non ullam et et sunt enim eligendi expedita qui voluptate occaecati error sunt accusamus assumenda animi sint dicta nobis maxime et qui sequi debitis ut molestiae dolore quia id vel sint ipsa optio eum nisi autem animi optio id quos illo blanditiis impedit quasi eveniet omnis non voluptas commodi non quo ex aut cupiditate voluptatem provident qui consectetur facilis ratione accusantium consequatur et assumenda saepe deleniti accusamus impedit non reprehenderit repellat qui atque adipisci est ipsum est vitae reiciendis consectetur dolores deleniti sapiente.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 28, Key = new Guid("4EA06D28-07FE-4E72-BFF4-BA2FBFB390C7"), Name = "Trilogy", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 4, Key  = new Guid("433470CD-6E7D-4D2D-9FE7-C0FA483E93FF"), Title  = "Rustic Incredible Soft Bike 949", Publisher  = "Quigley - Bartell", Language  = "PL", Isbn  = "14533162",Description  = "Ut consequatur quaerat ratione quaerat molestiae ut dolores saepe quae aut animi nesciunt et ea quam voluptate corporis rem ad qui fugiat delectus rem quibusdam ex fugiat facilis nulla quis voluptatem sit corporis ipsa sit eum soluta dignissimos cum cumque architecto animi voluptatem suscipit ut molestiae earum quos accusantium odit repudiandae inventore est quam officiis quasi nulla temporibus ut et deserunt deserunt nostrum cupiditate est voluptatum ullam fugit mollitia sit id sapiente fuga assumenda velit quibusdam excepturi et similique quis esse laboriosam et id blanditiis voluptatem id aut ipsum hic ut saepe aspernatur assumenda odit atque enim quibusdam est molestiae consequuntur dolorem saepe consequatur quis tenetur et tenetur eius beatae iste eius modi reiciendis quos magnam aut iste et laudantium accusamus quam rerum est deleniti nulla et vero distinctio porro corrupti porro sit eos et officia dignissimos est consequatur quas eum est nam explicabo qui quia tempore sed minima vero molestias possimus minus qui natus impedit aut dolore non cupiditate sunt in exercitationem et officia quo velit vitae minus et aut consequatur amet natus quidem eius facere pariatur aliquam sequi quia maxime culpa aut non minus maiores officiis in sed impedit velit beatae nulla esse magnam ex neque quas iste quia officia iure quis ab qui similique ipsum molestias nisi tenetur doloremque asperiores autem aliquid ut repellat corrupti nostrum vero at voluptas molestias velit velit est quis nisi ratione iure eos officia ut cumque aperiam officia ex quia possimus et neque vel consequatur ut culpa cum sed nihil quos quasi magnam et officiis facilis cumque quibusdam.", Quantity  = 30, Paperback  = 315, PublishDate  = new DateTime(1927,11,26), AuthorId  = 3,ImageFileId  = null, CategoryId  = 9, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis doloremque eligendi dolores et quibusdam quam in accusantium id reiciendis itaque maxime provident optio rerum illum repellendus non molestias ut cum quam a quos optio ad aut quis autem nulla consequatur optio ea reiciendis illo praesentium nesciunt sed quod rerum molestias possimus ea qui accusamus dolorem ut corrupti dolores nisi minus aut voluptatem a sed laborum id alias aut cupiditate velit fugit incidunt possimus ut et laboriosam iste facilis corrupti expedita libero consectetur natus reprehenderit sint inventore quas fuga officiis molestiae occaecati voluptatem ducimus fugit eveniet autem ducimus omnis odio quisquam nesciunt perferendis eius sed qui cupiditate animi deleniti fuga atque autem nulla facilis est veritatis et possimus error veniam omnis incidunt autem tempora mollitia necessitatibus sapiente repellendus facilis architecto accusamus saepe omnis sed assumenda et expedita aut accusantium repellat reprehenderit blanditiis accusantium repellat iure magni cum quas omnis illo aut et rerum quos necessitatibus corrupti dolor repellendus dolorum est et quod a voluptate possimus deserunt sit rerum debitis consectetur voluptate hic minus error necessitatibus explicabo quia autem voluptas laudantium non blanditiis accusantium officia aspernatur magnam aut veniam ipsam minima porro accusamus aliquid autem similique et dolores dolorum et quibusdam fugit aperiam et sunt est accusantium aut voluptatem animi autem veniam ea rerum nesciunt iusto aut porro quis nemo quas qui minus voluptas animi rem sed mollitia ex repellat labore mollitia officia dolores quae ut dolor eum blanditiis earum architecto consequatur est nobis cupiditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 9, Key = new Guid("45C914D1-4FAB-4650-B5C3-30CAC39BA7C9"), Name = "Health", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 5, Key  = new Guid("6968DB0F-6059-4B23-865A-317D70F46268"), Title  = "Intelligent Rustic Cotton Pizza 534", Publisher  = "Bailey Inc", Language  = "EN", Isbn  = "36237977",Description  = "Modi optio eaque sapiente enim et occaecati ea culpa pariatur beatae dolores asperiores porro cumque repellat labore quas dolorem doloremque ipsam numquam itaque temporibus dolore facere numquam asperiores voluptates consequatur qui beatae praesentium dolores est hic nam quo consequatur sunt expedita aperiam ad autem rerum voluptatibus atque est maiores ullam aut quaerat qui voluptas esse aut possimus dolore quo sint ea non dolores numquam sed repellat cum eos quasi placeat sit in optio mollitia autem veniam itaque natus voluptas voluptatem enim similique aut laborum voluptatem rem quaerat eligendi porro quidem consectetur quam quia nostrum dolorem consequuntur ut inventore quaerat hic nemo dolorum in ut delectus cumque error tempora iure dolores quasi velit ea ducimus dolore quo occaecati rerum tenetur facilis id voluptatibus ratione et iste repudiandae minima ea molestiae dolore maxime debitis ullam saepe a est doloribus est facilis natus voluptates aperiam corporis culpa similique asperiores perferendis tempore quae sit est et odit laborum non quia voluptatum qui cum illo nihil nesciunt quibusdam qui reprehenderit consectetur eveniet excepturi quos animi pariatur maxime et reprehenderit minus nihil cum error et error ut id ex explicabo quidem sunt est nobis soluta harum ipsum nulla aspernatur officia eos placeat ut neque consectetur autem explicabo quas sit ducimus atque eos velit porro debitis molestias facilis repudiandae minima aut sequi illum et nihil quas ullam quam non eum vero molestiae alias est et aliquam maiores rem sint quis quos rem qui nihil voluptates laborum omnis cum illo consequatur quia maiores quaerat similique maiores iusto voluptatem hic non expedita necessitatibus tenetur aut.", Quantity  = 5, Paperback  = 544, PublishDate  = new DateTime(1985,3,22), AuthorId  = 3,ImageFileId  = null, CategoryId  = 29, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis doloremque eligendi dolores et quibusdam quam in accusantium id reiciendis itaque maxime provident optio rerum illum repellendus non molestias ut cum quam a quos optio ad aut quis autem nulla consequatur optio ea reiciendis illo praesentium nesciunt sed quod rerum molestias possimus ea qui accusamus dolorem ut corrupti dolores nisi minus aut voluptatem a sed laborum id alias aut cupiditate velit fugit incidunt possimus ut et laboriosam iste facilis corrupti expedita libero consectetur natus reprehenderit sint inventore quas fuga officiis molestiae occaecati voluptatem ducimus fugit eveniet autem ducimus omnis odio quisquam nesciunt perferendis eius sed qui cupiditate animi deleniti fuga atque autem nulla facilis est veritatis et possimus error veniam omnis incidunt autem tempora mollitia necessitatibus sapiente repellendus facilis architecto accusamus saepe omnis sed assumenda et expedita aut accusantium repellat reprehenderit blanditiis accusantium repellat iure magni cum quas omnis illo aut et rerum quos necessitatibus corrupti dolor repellendus dolorum est et quod a voluptate possimus deserunt sit rerum debitis consectetur voluptate hic minus error necessitatibus explicabo quia autem voluptas laudantium non blanditiis accusantium officia aspernatur magnam aut veniam ipsam minima porro accusamus aliquid autem similique et dolores dolorum et quibusdam fugit aperiam et sunt est accusantium aut voluptatem animi autem veniam ea rerum nesciunt iusto aut porro quis nemo quas qui minus voluptas animi rem sed mollitia ex repellat labore mollitia officia dolores quae ut dolor eum blanditiis earum architecto consequatur est nobis cupiditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 29, Key = new Guid("51E555F3-7033-407C-9DC1-75EA8279F1D6"), Name = "Biographies", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 6, Key  = new Guid("A63DAEB5-C6CB-4F63-9F36-46E1617CF6EA"), Title  = "Incredible Small Granite Bike 465", Publisher  = "Koelpin, Tremblay and Barton", Language  = "PL", Isbn  = "57673807",Description  = "Dolorum commodi ea expedita accusamus dolorem iusto distinctio numquam recusandae aperiam earum vero atque sed officia est aut necessitatibus magni natus sint mollitia repudiandae eos vero quisquam dolor ut cum non illo iusto non ut officia voluptatum sit qui at quae omnis perspiciatis cupiditate vero explicabo ad inventore dolore ab doloremque nostrum alias sapiente ipsum nihil assumenda quos sint perferendis dignissimos et ipsam sed consequatur voluptatibus quas molestiae numquam rerum aut impedit est eaque dicta consequatur autem quasi illo quae culpa nam provident exercitationem facilis rerum illo voluptatum omnis error qui maiores et iusto voluptate reprehenderit labore consequatur occaecati debitis officia ut voluptas ullam itaque quia rerum sapiente debitis neque voluptas cumque odio totam iusto dolor sed voluptatem saepe commodi molestiae doloremque dolorem est consequuntur consequuntur quia non et eligendi assumenda quis unde quia blanditiis incidunt qui ut ex quia alias explicabo animi mollitia labore quia perspiciatis doloremque alias et enim numquam fugit sequi tenetur natus dolor adipisci quia vel sed molestiae sit sit qui a voluptatem ut perferendis sed a rerum nam similique commodi eum molestiae sunt velit consequatur qui quia non alias nihil omnis quaerat reprehenderit eius eveniet ab et inventore perspiciatis quia sunt omnis quam velit minima ut minima sit et libero eum omnis perspiciatis temporibus quia magni est dolor dolorem incidunt earum eius sit possimus nulla cumque possimus ab fugit voluptatem sit eum quia commodi autem quasi aut quis magni eaque corporis nesciunt voluptate sit eaque alias quo animi explicabo assumenda dignissimos nulla necessitatibus ipsam voluptatem ex molestiae distinctio ex culpa non.", Quantity  = 41, Paperback  = 352, PublishDate  = new DateTime(1942,4,13), AuthorId  = 3,ImageFileId  = null, CategoryId  = 11, CreatedDate  = new DateTimeOffset(new DateTime(2017,10,11)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,10,11)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis doloremque eligendi dolores et quibusdam quam in accusantium id reiciendis itaque maxime provident optio rerum illum repellendus non molestias ut cum quam a quos optio ad aut quis autem nulla consequatur optio ea reiciendis illo praesentium nesciunt sed quod rerum molestias possimus ea qui accusamus dolorem ut corrupti dolores nisi minus aut voluptatem a sed laborum id alias aut cupiditate velit fugit incidunt possimus ut et laboriosam iste facilis corrupti expedita libero consectetur natus reprehenderit sint inventore quas fuga officiis molestiae occaecati voluptatem ducimus fugit eveniet autem ducimus omnis odio quisquam nesciunt perferendis eius sed qui cupiditate animi deleniti fuga atque autem nulla facilis est veritatis et possimus error veniam omnis incidunt autem tempora mollitia necessitatibus sapiente repellendus facilis architecto accusamus saepe omnis sed assumenda et expedita aut accusantium repellat reprehenderit blanditiis accusantium repellat iure magni cum quas omnis illo aut et rerum quos necessitatibus corrupti dolor repellendus dolorum est et quod a voluptate possimus deserunt sit rerum debitis consectetur voluptate hic minus error necessitatibus explicabo quia autem voluptas laudantium non blanditiis accusantium officia aspernatur magnam aut veniam ipsam minima porro accusamus aliquid autem similique et dolores dolorum et quibusdam fugit aperiam et sunt est accusantium aut voluptatem animi autem veniam ea rerum nesciunt iusto aut porro quis nemo quas qui minus voluptas animi rem sed mollitia ex repellat labore mollitia officia dolores quae ut dolor eum blanditiis earum architecto consequatur est nobis cupiditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 11, Key = new Guid("A65E7C3B-CCDF-40D9-92C8-5ACCB9580BF0"), Name = "Travel", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 7, Key  = new Guid("C0A39D4E-CBAC-4768-A42F-D93D258934D3"), Title  = "Unbranded Handcrafted Plastic Towels 622", Publisher  = "Davis - Zulauf", Language  = "PL", Isbn  = "12619780",Description  = "Porro nulla maiores nesciunt tenetur ut impedit atque itaque consequatur aspernatur iusto rerum aliquid sit omnis dolorem architecto magnam omnis doloribus atque sunt provident id eaque est quia velit omnis iure deleniti unde molestiae earum molestias mollitia illo ad iste earum vero dolorum dolorum vitae natus velit assumenda eius aspernatur corporis pariatur provident ab aut ut ad qui omnis repudiandae optio et quis quia quam dolorum necessitatibus non voluptate molestias tenetur est et optio illum dolores et hic qui ipsa et aut doloribus aspernatur blanditiis sed hic dolores quam impedit ut qui et ea vero odio similique dignissimos sed praesentium libero sed ea impedit qui provident ab quo incidunt officiis quis quia repellat excepturi quis illum excepturi amet quisquam aut tempore reprehenderit autem cumque ad vel ad odio quo autem error quam placeat asperiores quis nihil occaecati minima tenetur molestiae corrupti aut ut unde suscipit repudiandae qui maiores architecto ipsa non eum quisquam quia temporibus ad velit expedita iusto sit quaerat laborum alias at enim hic in consequatur eos eos exercitationem ut ut quibusdam qui natus consequuntur dolore commodi ea libero quis consequatur autem ducimus sit cumque nihil sed explicabo perferendis et voluptas fuga libero officiis aut velit ut iure suscipit dolores nulla non maxime optio ratione molestiae molestiae incidunt voluptatem dolorem nostrum architecto distinctio ut dolore quia voluptate consequuntur magnam voluptatem aut quae pariatur et reprehenderit est magni occaecati ducimus quo aut reiciendis accusantium sed odio sequi ab facere aperiam omnis et aut rerum pariatur sunt assumenda sapiente optio fuga et consequatur aut eos amet.", Quantity  = 25, Paperback  = 655, PublishDate  = new DateTime(1952,1,19), AuthorId  = 3,ImageFileId  = null, CategoryId  = 13, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis doloremque eligendi dolores et quibusdam quam in accusantium id reiciendis itaque maxime provident optio rerum illum repellendus non molestias ut cum quam a quos optio ad aut quis autem nulla consequatur optio ea reiciendis illo praesentium nesciunt sed quod rerum molestias possimus ea qui accusamus dolorem ut corrupti dolores nisi minus aut voluptatem a sed laborum id alias aut cupiditate velit fugit incidunt possimus ut et laboriosam iste facilis corrupti expedita libero consectetur natus reprehenderit sint inventore quas fuga officiis molestiae occaecati voluptatem ducimus fugit eveniet autem ducimus omnis odio quisquam nesciunt perferendis eius sed qui cupiditate animi deleniti fuga atque autem nulla facilis est veritatis et possimus error veniam omnis incidunt autem tempora mollitia necessitatibus sapiente repellendus facilis architecto accusamus saepe omnis sed assumenda et expedita aut accusantium repellat reprehenderit blanditiis accusantium repellat iure magni cum quas omnis illo aut et rerum quos necessitatibus corrupti dolor repellendus dolorum est et quod a voluptate possimus deserunt sit rerum debitis consectetur voluptate hic minus error necessitatibus explicabo quia autem voluptas laudantium non blanditiis accusantium officia aspernatur magnam aut veniam ipsam minima porro accusamus aliquid autem similique et dolores dolorum et quibusdam fugit aperiam et sunt est accusantium aut voluptatem animi autem veniam ea rerum nesciunt iusto aut porro quis nemo quas qui minus voluptas animi rem sed mollitia ex repellat labore mollitia officia dolores quae ut dolor eum blanditiis earum architecto consequatur est nobis cupiditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 13, Key = new Guid("6307D58F-E2BE-466B-89FC-53147B64283D"), Name = "Religion, Spirituality & New Age", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 8, Key  = new Guid("E11EBF20-3D98-43A9-B711-63F85C05A36B"), Title  = "Licensed Intelligent Plastic Chicken 131", Publisher  = "OConnell - Pfeffer", Language  = "DE", Isbn  = "64012402",Description  = "Ut perferendis ut architecto earum laudantium esse cum labore et quidem et excepturi enim vero expedita pariatur numquam quasi in beatae itaque sit unde amet laudantium dolorem sed sequi quos quis sint non est voluptas illo amet voluptatibus at nisi quibusdam nobis et eveniet perferendis assumenda molestiae autem natus vel ipsam ab vel nam sed sapiente soluta nihil rerum voluptatum quidem aut occaecati placeat minima porro doloribus id maiores et rem accusamus ea est id reprehenderit quas iusto provident esse aperiam ducimus reiciendis omnis dolor quia ut ut sunt commodi autem omnis explicabo vel impedit sint neque nobis et ullam omnis eligendi reprehenderit possimus voluptates dignissimos est expedita veritatis consequatur praesentium dolore necessitatibus totam odio amet et maxime sunt a dolor est dolorem dolorum molestiae impedit est voluptatibus sit nobis incidunt qui illum ut rerum nisi inventore eius accusamus minus harum rerum sed omnis et consequuntur accusamus fuga itaque occaecati perferendis esse aut distinctio sed illo non est sit velit tempore impedit rerum autem culpa quidem molestiae expedita eum aspernatur magni nobis debitis voluptas voluptas exercitationem occaecati ea porro ullam quo voluptatem ipsa dolores tempore vel impedit nesciunt fugit officia illum quis rerum libero inventore et dolore eum dolor enim optio numquam autem voluptas reprehenderit voluptas ut enim sit dolorem provident quia inventore aperiam ea cupiditate nam qui cumque assumenda molestiae enim perspiciatis et veniam nulla et quia dolor ea ab eligendi tempora ducimus animi fuga ex sed odit ut enim odio dolorem quis illo eligendi incidunt at enim sed quaerat aut consequuntur velit nisi molestiae.", Quantity  = 79, Paperback  = 856, PublishDate  = new DateTime(1984,12,16), AuthorId  = 4,ImageFileId  = null, CategoryId  = 2, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 4, Key = new Guid("2E218BD7-D56D-428E-8765-64C3825D3EEF"), FirstName = "Princess", LastName = "Hessel",Description = "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 2, Key = new Guid("4D4393B4-A6F6-40AB-A18D-96703462496E"), Name = "Satire", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 9, Key  = new Guid("CDD053B4-6E2C-4CA1-8B17-BD1A3B919A51"), Title  = "Awesome Awesome Rubber Hat 62", Publisher  = "Kuhn - Ward", Language  = "DE", Isbn  = "88647140",Description  = "Ex dolores corrupti ducimus aliquam amet officia et rerum eveniet et ratione rerum error illum aut quia tenetur et rerum iste qui itaque rerum saepe excepturi numquam quibusdam dolorum nihil sunt consequatur autem veritatis est repudiandae at voluptatibus esse commodi ut recusandae culpa iusto dolore repellendus eum ad vitae molestias quaerat dolor consequatur qui deleniti sed iste ut omnis maxime aut autem asperiores cupiditate quia debitis aliquid aut autem ut magnam adipisci ut odio tempore fuga labore quas cumque aut soluta itaque porro aut neque autem alias aut qui ut ex enim molestiae libero reprehenderit dolor itaque illo fugit quis et rerum atque dolor aliquam veritatis labore non voluptatem at exercitationem illo voluptas odio veritatis aperiam beatae aut ea non debitis vel ullam iste unde itaque voluptas officiis quae quos est amet iure sit soluta id qui sed vero aspernatur omnis architecto qui qui earum ullam rem doloribus est ex aspernatur illo eligendi qui dolores illo est non harum voluptatem ullam consequatur aut expedita dolorem odit nam excepturi omnis voluptas qui ut accusantium quaerat aut aut iste similique laborum dolore totam omnis voluptatem eligendi qui quidem quos aut quo et sunt cupiditate et ut et amet consectetur dolorem error fugiat harum sapiente tempora veniam est natus accusantium et est eligendi dolores beatae ipsam eum pariatur cupiditate aut amet voluptatem provident et eos tempore dolores labore inventore quidem corrupti praesentium temporibus laborum reprehenderit consequatur dolor et molestias unde voluptatibus et fugiat est inventore adipisci ratione magni aut non cupiditate est minima vitae alias et aperiam eius est.", Quantity  = 41, Paperback  = 350, PublishDate  = new DateTime(1947,10,21), AuthorId  = 4,ImageFileId  = null, CategoryId  = 10, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 4, Key = new Guid("2E218BD7-D56D-428E-8765-64C3825D3EEF"), FirstName = "Princess", LastName = "Hessel",Description = "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 10, Key = new Guid("E7F716C2-FA5E-433C-A3C3-478F1B94E44B"), Name = "Guide", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 10, Key  = new Guid("8EFA1076-7905-45DB-A8CB-CDEB792C1442"), Title  = "Ergonomic Small Metal Sausages 951", Publisher  = "Wiza - Schuppe", Language  = "PL", Isbn  = "10573912",Description  = "Molestiae et iste nam non alias tempora pariatur fugit et sit impedit est corrupti aut natus voluptas modi qui itaque quia repudiandae architecto dicta autem et accusantium earum tempore facere ut ullam sapiente minus voluptatum fugit distinctio odit possimus explicabo illum et consequatur id autem fugit totam et explicabo aut molestiae sed cupiditate amet veritatis sint nulla rem nostrum praesentium aut veniam quia ea qui aut placeat officiis aut eos eveniet id laudantium ea sed laboriosam aut suscipit exercitationem voluptatem mollitia voluptate nulla cumque eum tempora omnis est tempore sapiente ut consectetur vel ullam sed sed eligendi qui veniam voluptates commodi deserunt est ut architecto amet expedita dolorem et est velit molestias quia accusantium eos error odio rem non tempora odit aut sed quae mollitia impedit molestiae neque labore qui facilis est illo eum eveniet consequatur et laborum fuga rerum similique laborum impedit rerum unde possimus perferendis repellendus iusto omnis aspernatur modi et inventore aut reprehenderit iure natus quidem consequatur voluptatem quos molestiae atque beatae voluptas blanditiis autem magni vero voluptas dolorem nulla necessitatibus expedita quos voluptatem aliquam et voluptas eligendi eos qui est corrupti nihil qui repellat in quas et labore aut quaerat vel architecto est modi et repellendus ullam modi expedita in temporibus enim autem aperiam in enim magni dolores veritatis quibusdam cum consequuntur voluptas ut repellat expedita eaque praesentium numquam fuga et cupiditate at officia molestias qui aut eaque reprehenderit quo ut totam deleniti sequi eveniet numquam quae eligendi animi voluptatem autem atque dolor quia culpa aliquam enim qui tempora quia maxime voluptas.", Quantity  = 53, Paperback  = 600, PublishDate  = new DateTime(1920,10,13), AuthorId  = 4,ImageFileId  = null, CategoryId  = 15, CreatedDate  = new DateTimeOffset(new DateTime(2017,10,9)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,10,9)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 4, Key = new Guid("2E218BD7-D56D-428E-8765-64C3825D3EEF"), FirstName = "Princess", LastName = "Hessel",Description = "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 15, Key = new Guid("0E63BE9D-37C9-4C38-86FC-68A85593CD93"), Name = "History", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 11, Key  = new Guid("D1BBA9A4-5613-480C-9424-FDB4933D2E82"), Title  = "Practical Sleek Rubber Sausages 181", Publisher  = "Wunsch, Kris and Koepp", Language  = "EN", Isbn  = "12773876",Description  = "Velit placeat reiciendis eum in voluptatem aut voluptatem aut nemo nihil nobis ipsum est vero eaque voluptatibus amet sit repellat minima delectus ex consequatur eligendi accusamus ut vitae accusamus reprehenderit ad velit aut aut dolor inventore quisquam fugit quae ut consequatur optio animi cum omnis dolor amet unde ea qui placeat sed pariatur et commodi incidunt reprehenderit dolorem eos provident sit quisquam reprehenderit quisquam aperiam nostrum impedit quis totam et ipsam et qui tempore consectetur qui consequatur cum doloremque voluptates nobis quam soluta qui rerum quasi error quia occaecati rerum nam iste reiciendis sint neque dicta corporis aut facilis atque architecto eveniet beatae id esse aut inventore quia est omnis et quo eos tempora pariatur voluptas ipsa dolore beatae sapiente qui provident assumenda occaecati dolores omnis necessitatibus et molestias aut minima corrupti aperiam unde quo repellendus laboriosam beatae qui omnis perferendis iure error quod vitae hic quam facere libero in quae illo et soluta possimus ut consequuntur natus totam et asperiores et libero molestias dolores vel cum officia iure veritatis nihil delectus occaecati voluptatem hic laboriosam ut doloremque odio sit nisi dolores a aut illo tempora nobis ex temporibus ullam expedita dolor est architecto voluptas culpa quisquam inventore et nihil enim eum consequatur id reiciendis vitae sed atque minus esse sunt aspernatur sed veniam enim eos rem natus qui enim eligendi quia et dicta ea facilis ex ducimus omnis consequatur dolorum voluptates repellendus id repudiandae sint assumenda ex magni aut est sed dolorum autem qui iste distinctio quia voluptas et odit neque id ut aut rerum.", Quantity  = 82, Paperback  = 512, PublishDate  = new DateTime(1922,8,20), AuthorId  = 5,ImageFileId  = null, CategoryId  = 27, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 5, Key = new Guid("D2740B82-FD37-4A42-8ACA-0A6A61615645"), FirstName = "Alexandria", LastName = "Hartmann",Description = "Dolor sunt sint id repellendus ut perspiciatis sequi ad natus vel iste placeat adipisci autem nihil voluptatibus ex quia beatae adipisci sapiente voluptatem sint omnis harum est sunt et consequatur qui error porro ea libero dolore quo veritatis rem ut labore id aut dolor ducimus ea fuga esse voluptates voluptate laudantium labore incidunt et et qui omnis ea reiciendis voluptatem dolorum aut repudiandae voluptates reiciendis vel quo ut non perspiciatis asperiores et aut enim quo officia tenetur nulla et in consequatur incidunt distinctio fugiat et nisi suscipit vel sed nulla optio enim dicta quia veniam ut est aut velit harum occaecati minima rerum commodi esse ut ea a et ad et nihil suscipit in qui iure enim beatae alias harum deleniti accusantium consequatur soluta pariatur soluta fugiat natus maiores sunt ut pariatur facilis et debitis qui ut ullam quam commodi maiores autem placeat voluptatem voluptatibus doloribus aut molestias dolor eveniet tenetur laborum aspernatur similique sequi dolores totam quia sint minus nihil quibusdam ut dolores autem sit veritatis ut optio velit ipsa perferendis expedita quidem facilis aut minima unde cumque vero impedit voluptas eum sunt et nostrum placeat beatae minima eum voluptatem qui dolor est expedita consectetur voluptate aliquam necessitatibus delectus non quasi vel voluptas at earum tenetur praesentium et illo eius sint quas distinctio accusantium vel esse aut debitis vitae ut maiores fuga ipsam voluptatem eveniet incidunt ut voluptatibus blanditiis optio voluptatem distinctio id molestiae quidem sint et ut cum suscipit quis sequi tempora neque nisi quidem quia nam ad est cupiditate autem aut cumque rerum.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 27, Key = new Guid("94A01CBA-0011-4BD3-BBAC-EE83B17068E2"), Name = "Series", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 29, Key  = new Guid("D68D5372-2E78-4C92-BE84-D34F240BF75A"), Title  = "Rustic Unbranded Fresh Fish 880", Publisher  = "Kutch - Volkman", Language  = "PL", Isbn  = "10579841",Description  = "Fuga odit maiores qui quidem molestiae rerum quae adipisci quaerat suscipit ipsum sunt et inventore quod consequuntur et libero vero voluptatum est cumque laborum reprehenderit et quaerat voluptatem aut nisi est quae consectetur cum ea eum ab et voluptatem dolore illo temporibus sint consectetur dolorem quod animi ad alias voluptas dolorum eaque sit harum rerum itaque ea quis facere fugiat nam fuga itaque pariatur atque nostrum voluptatibus eveniet molestias tempora eligendi aut facere quam aut est ipsum molestias ducimus est sed maiores quaerat ut quisquam id et natus aperiam quam nihil cum quos voluptatem qui molestiae at nam impedit debitis omnis cumque assumenda est voluptatem quod veniam omnis dolorem hic voluptas eveniet molestiae aut et soluta consequatur libero consequuntur maxime nihil nihil eos dicta aut sit reprehenderit iusto quo vel quo molestias et sunt necessitatibus et eius corrupti sed voluptatibus blanditiis aut minus vitae et non placeat iure est ut veritatis sit sed sapiente qui laboriosam voluptas soluta ut dolores ut nulla molestiae ducimus fugit quaerat aspernatur ex numquam consequuntur laudantium aut omnis ratione nostrum voluptatem eligendi omnis iusto ad ut alias maxime recusandae ipsam et ut quo sed omnis distinctio sed sint maiores harum est quia veniam ut qui et qui quia natus iusto sit et reprehenderit tempore magni facilis veritatis dolore optio et ut aspernatur asperiores dolores laudantium quis quaerat ut distinctio totam molestiae commodi similique excepturi soluta qui velit consequatur eaque nulla officiis dolorem sit dolores dolores voluptatem culpa neque doloribus et mollitia nostrum qui sed quis molestias sit ratione quas odit ipsum.", Quantity  = 32, Paperback  = 128, PublishDate  = new DateTime(1935,8,7), AuthorId  = 11,ImageFileId  = null, CategoryId  = 4, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Pending", Order  = new List<Order>(),Author  = new Author() { Id = 11, Key = new Guid("E9862AF8-0017-4C36-B424-68E2D39D10DF"), FirstName = "Arvel", LastName = "Greenfelder",Description = "Et qui dolorem ratione aut quisquam voluptatum quaerat est omnis non nostrum qui suscipit vero eius maxime a enim sint ab odit laudantium provident et ut eos facilis officia et atque dignissimos rerum neque et sapiente rem nesciunt velit veritatis consequatur distinctio quibusdam unde consectetur at soluta officia tempora ex dolores ut dolorum nobis id molestias soluta nostrum et nihil eos eum tempore nesciunt minima dicta voluptate quam ut saepe placeat officia possimus quia excepturi reprehenderit enim repudiandae laboriosam illo qui ad esse incidunt sed eos qui velit molestiae iste veniam dolor sint quos sed quia doloribus quidem enim odit dignissimos voluptatem voluptatem suscipit est voluptates officia velit quia ut velit voluptas tenetur consectetur sed qui esse odio eaque qui voluptatem quia doloribus impedit est sunt tempore voluptatem consequatur laboriosam in tempore facilis ut est quia a vitae fuga non unde velit accusantium vitae ea occaecati modi est quis dolorum omnis omnis animi magni voluptas ut enim est est eaque ut quia voluptate fugiat rerum cupiditate quia quisquam culpa quibusdam et sit illum eum deserunt at sit voluptates aliquid sunt ut rerum necessitatibus iure culpa pariatur architecto excepturi reprehenderit quis illo ut ex qui aut tempora consequatur consectetur dolores quo qui quas ratione in doloribus molestiae minus assumenda ut doloribus illo ea quaerat quidem est nostrum et voluptatem aliquam aspernatur ducimus est voluptas quia quod veniam reiciendis incidunt vero consequatur pariatur asperiores omnis excepturi eligendi voluptas quibusdam ipsam qui voluptatem fugit corporis aut ea voluptatem eligendi sunt porro saepe nobis autem voluptatibus recusandae aperiam iure repellendus.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 4, Key = new Guid("F42E66C2-89CE-4D27-92CF-2348B8B21348"), Name = "Action and Adventure", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 30, Key  = new Guid("657E8BB0-3F33-4A92-B05E-43D977CBBC44"), Title  = "Sleek Ergonomic Granite Bacon 151", Publisher  = "Thiel Group", Language  = "DE", Isbn  = "22289594",Description  = "Alias odio impedit aspernatur dolore possimus accusamus harum perspiciatis ducimus modi ipsum a voluptatem corporis non tempore quo ipsam sunt maiores ipsam sapiente cumque dolorem alias libero nisi dicta ut consequuntur voluptas deserunt est ratione et in quasi repudiandae possimus modi debitis ad laborum et dolorem doloremque aut hic minima dolor veniam quia dolorem molestias nobis voluptatum consequatur non dolore dicta cupiditate unde sunt quia blanditiis in enim fugit ut non animi quibusdam iste et aut eaque aliquid quae nisi aut optio qui aut autem voluptas natus assumenda sit laudantium deserunt reprehenderit qui libero tempore cumque voluptas cumque quis adipisci dolore omnis eos quo placeat in corrupti est sapiente qui facilis consequuntur sapiente officia enim reprehenderit consequuntur quod dolor deleniti eveniet consequatur dicta est facilis dolores praesentium consequuntur rerum neque dolor suscipit veritatis pariatur explicabo numquam fugiat nobis inventore eius ex repudiandae aut repellendus illum laudantium et iste qui minima sunt qui et consequatur ullam odio tempore non et laborum fugit ea voluptas quibusdam laborum nemo et aut quos inventore iste dignissimos nobis error consequuntur maiores alias ullam fugiat dolor nihil et minus id deserunt adipisci esse aliquam unde et ut fuga et dignissimos aut repellat ratione aut ipsum molestias in voluptatem veritatis suscipit est magni ut ea soluta ipsam odio quis autem rerum voluptates quia et nesciunt sed esse saepe consequatur error quo occaecati recusandae dolorem labore praesentium consequuntur quo doloribus consequuntur quia dolor eum nulla qui aut laudantium exercitationem illo sunt ut numquam dolorem dolore qui sed eveniet consequatur doloribus aut facere laborum officia.", Quantity  = 11, Paperback  = 368, PublishDate  = new DateTime(1993,6,30), AuthorId  = 11,ImageFileId  = null, CategoryId  = 4, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Inactive", Order  = new List<Order>(),Author  = new Author() { Id = 11, Key = new Guid("E9862AF8-0017-4C36-B424-68E2D39D10DF"), FirstName = "Arvel", LastName = "Greenfelder",Description = "Et qui dolorem ratione aut quisquam voluptatum quaerat est omnis non nostrum qui suscipit vero eius maxime a enim sint ab odit laudantium provident et ut eos facilis officia et atque dignissimos rerum neque et sapiente rem nesciunt velit veritatis consequatur distinctio quibusdam unde consectetur at soluta officia tempora ex dolores ut dolorum nobis id molestias soluta nostrum et nihil eos eum tempore nesciunt minima dicta voluptate quam ut saepe placeat officia possimus quia excepturi reprehenderit enim repudiandae laboriosam illo qui ad esse incidunt sed eos qui velit molestiae iste veniam dolor sint quos sed quia doloribus quidem enim odit dignissimos voluptatem voluptatem suscipit est voluptates officia velit quia ut velit voluptas tenetur consectetur sed qui esse odio eaque qui voluptatem quia doloribus impedit est sunt tempore voluptatem consequatur laboriosam in tempore facilis ut est quia a vitae fuga non unde velit accusantium vitae ea occaecati modi est quis dolorum omnis omnis animi magni voluptas ut enim est est eaque ut quia voluptate fugiat rerum cupiditate quia quisquam culpa quibusdam et sit illum eum deserunt at sit voluptates aliquid sunt ut rerum necessitatibus iure culpa pariatur architecto excepturi reprehenderit quis illo ut ex qui aut tempora consequatur consectetur dolores quo qui quas ratione in doloribus molestiae minus assumenda ut doloribus illo ea quaerat quidem est nostrum et voluptatem aliquam aspernatur ducimus est voluptas quia quod veniam reiciendis incidunt vero consequatur pariatur asperiores omnis excepturi eligendi voluptas quibusdam ipsam qui voluptatem fugit corporis aut ea voluptatem eligendi sunt porro saepe nobis autem voluptatibus recusandae aperiam iure repellendus.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 4, Key = new Guid("F42E66C2-89CE-4D27-92CF-2348B8B21348"), Name = "Action and Adventure", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 31, Key  = new Guid("2E6C91BE-A96F-4F72-9644-90D0EEC7DEE3"), Title  = "Rustic Unbranded Wooden Hat 391", Publisher  = "Legros, Cormier and Nikolaus", Language  = "DE", Isbn  = "45611258",Description  = "Quibusdam optio nulla reprehenderit aperiam dolorem adipisci minima voluptas illum omnis sequi blanditiis consectetur in aliquid ex quo ut veniam asperiores omnis fugiat nihil consequatur voluptatibus nihil et dolores voluptatem vero eos repellat quasi velit ducimus expedita ipsam vitae possimus et et numquam quo iure corrupti sint corporis earum quia autem ex rerum dolore voluptas dolore et eos voluptatem illo quod ut ratione laborum sapiente ut nesciunt iure omnis consequuntur optio eaque facere officia ea placeat suscipit quia voluptatem aut totam sed quo consequatur ex et aut placeat tempora neque qui sint nostrum doloremque perferendis eum aspernatur animi facere corrupti sit magni non vel et facere id ullam necessitatibus sunt repudiandae maxime qui qui qui fuga eum suscipit quaerat praesentium non voluptate nesciunt laborum perspiciatis dolor placeat est sunt reiciendis et id quis fugiat omnis laboriosam deserunt eveniet sit animi eveniet cum eos dolores magnam voluptas optio voluptatem dignissimos reprehenderit iste omnis nisi nostrum natus et ut repudiandae enim ducimus consequatur debitis est voluptates odio quia quis doloribus deserunt dolore doloremque non voluptatem expedita error magnam perspiciatis voluptatem ea nihil deleniti mollitia minima dolor quam necessitatibus qui beatae ad et consequatur cum et id mollitia sit hic hic optio beatae totam impedit natus velit commodi fuga est nobis est nemo vel rerum omnis similique ut voluptate animi perspiciatis corrupti unde dolor nostrum porro sapiente aut aliquid corporis esse veritatis unde corporis mollitia voluptas veniam ut est eaque vitae illum quibusdam minima impedit ex delectus magnam nihil consequuntur veritatis voluptas amet esse voluptate praesentium ratione ullam quia.", Quantity  = 7, Paperback  = 223, PublishDate  = new DateTime(2016,4,9), AuthorId  = 12,ImageFileId  = null, CategoryId  = 7, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Deleted", Order  = new List<Order>(),Author  = new Author() { Id = 12, Key = new Guid("D47AD167-7C15-4CAA-BB69-53C5EF0CA20D"), FirstName = "Dane", LastName = "Schumm",Description = "Harum ut accusamus laboriosam sint eveniet maxime expedita consectetur culpa corrupti est modi sunt vero molestiae quos voluptatum optio doloremque fuga eos sed quod itaque voluptas adipisci sit aut voluptatibus tenetur soluta vel modi quia et illo at nihil aut temporibus deleniti sint atque ratione vitae consequuntur vero maxime eaque et qui quaerat enim ipsum quos ratione magni in beatae cumque est unde non et aut debitis alias ducimus ea autem rerum est officia labore ratione dolorum quidem mollitia vero quo laboriosam enim architecto consequatur tempora nihil quas ab harum aliquid et qui et voluptate veritatis recusandae corporis at nisi quia consequuntur voluptate iure nam aut a libero quasi accusantium officiis excepturi sed perspiciatis commodi commodi blanditiis voluptatum nulla ea vel quis tenetur illum aut qui amet ratione dolor quia numquam ipsam dolor magnam est officia recusandae rerum voluptate reprehenderit ut facere et voluptatem possimus quo iure et voluptatem quae est praesentium praesentium qui sapiente fugiat quia deserunt vel non temporibus vel quos saepe vel iste qui veniam eveniet omnis et similique et blanditiis dolores fuga itaque non inventore ipsum veritatis veniam aut dolores similique facere vero voluptas nostrum vel velit laboriosam molestias velit animi blanditiis voluptatem ut quia sed aut odit aut atque et eaque qui alias voluptatem suscipit saepe et provident non ratione molestias non ex voluptas inventore cumque ea doloribus vitae consequatur sunt veritatis et at cum commodi in est vero veritatis tenetur enim in occaecati non ea at aut tenetur sint ipsum consequatur ullam ab explicabo cupiditate et non ratione quas nihil.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 7, Key = new Guid("CEB7EF1E-AA0E-4D91-9374-A15F028F3BEA"), Name = "Horror", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
            };

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);

            foreach (var book in _data)
            {
                context.Book.Add(book);
            }
            context.SaveChanges();
            _context = context;
        }

        [TestCase("D1BBA9A4-5613-480C-9424-FDB4933D2E82", 11)]
        [TestCase("E11EBF20-3D98-43A9-B711-63F85C05A36B", 8)]
        [TestCase("433470CD-6E7D-4D2D-9FE7-C0FA483E93FF", 4)]
        [TestCase("8BEB61E6-50CA-417E-9784-690E32B905F6", 3)]
        public async Task Should_Return_Book_With_Specified_Key(string bookKey, int expectedId)
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var book = await booksDataService.GetBookByKey(bookKey);
            Assert.NotNull(book);
            Assert.AreEqual(expectedId, book.Id);
        }

        [TestCase("wrongBookKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        public async Task Should_Return_Null_When_Book_Key_Is_Wrong(string bookKey)
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var book = await booksDataService.GetBookByKey(bookKey);
            Assert.Null(book);
        }

        [Test]
        public void Should_Throw_Exception_When_Book_Key_Is_Null()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.GetBookByKey(null));
        }

        [Test]
        public async Task Should_Return_Null_When_Book_Status_Is_Deleted()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var book = await booksDataService.GetBookByKey("2E6C91BE-A96F-4F72-9644-90D0EEC7DEE3");
            Assert.Null(book);
        }

        [Test]
        public void Should_Throw_Exception_When_Book_Key_Is_Empty()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.GetBookByKey(string.Empty));
        }

        [Test]
        public async Task Should_Create_New_Book()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            var actualBook = await booksDataService.CreateBook(expectedBook);

            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(Status.Active.ToString(), actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
         }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Title_Already_Exist()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Licensed Tasty Wooden Bike 78",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Isbn_Already_Exist()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Licensed Tasty Wooden Bike 78",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "40446591",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Title_Already_Exist_With_Status_Pending()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Fresh Fish 880",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Isbn_Already_Exist_With_Status_Pending()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Fresh Fish 880",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "10579841",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Title_Already_Exist_With_Status_Inactive()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Sleek Ergonomic Granite Bacon 151",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Isbn_Already_Exist_With_Status_Inactive()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Fresh Fish 880",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "22289594",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public async Task Should_Create_New_Book_When_Book_With_Title_Already_Exist_With_Status_Deleted()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Wooden Hat 391",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            var actualBook = await booksDataService.CreateBook(expectedBook);

            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(Status.Active.ToString(), actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public async Task Should_Create_New_Book_When_Book_With_Isbn_Already_Exist_With_Status_Deleted()
        {
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Intelligent Unbranded Concrete Computer 320",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "45611258",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description =
                            "Ea excepturi labore minus quaerat voluptatum quia et saepe ullam esse quia deserunt quos natus rerum deserunt corporis id ut consequatur ut ea autem ex aut quia nemo nihil itaque molestiae quis omnis est fuga culpa tenetur omnis iusto dolor excepturi sint ad ut sit qui nihil quia ut vero odit veritatis pariatur repellendus vitae explicabo voluptatem ducimus autem autem corporis eveniet numquam tenetur officiis magni ducimus maiores et voluptatibus est illum et dolorem iure necessitatibus doloribus consequuntur praesentium non blanditiis repellat laboriosam pariatur eum commodi odio reiciendis cupiditate provident molestiae quas in inventore deleniti laborum nihil voluptatem ea sed optio odio nemo maiores ipsum quis velit voluptatem qui adipisci aut accusantium maiores dolorem et corporis natus consequatur consequuntur molestias eligendi rem praesentium suscipit iste sit quam aut nisi omnis sit tempore voluptas quaerat excepturi perferendis eligendi aliquid cum ut dolorem esse qui sit et ea sint incidunt eos quia illo laboriosam consectetur at molestias tempora reiciendis ut et doloribus accusamus reprehenderit cum dolorum aut molestiae quaerat qui et et laborum non est officia nisi minima necessitatibus magni et sequi nam consectetur libero ipsum est nulla deleniti repudiandae placeat quis eaque quisquam sint est tempore omnis id sapiente voluptatem alias qui temporibus in incidunt error voluptas est id molestias incidunt laboriosam nulla et minima quia cum quisquam molestias saepe dolorem hic ut voluptatibus commodi accusamus saepe nostrum alias repudiandae similique ut molestiae consequatur ut aut debitis laborum odio est aut adipisci officiis ea sint hic consectetur et rerum consequatur asperiores illo tempora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };
            var actualBook = await booksDataService.CreateBook(expectedBook);

            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(Status.Active.ToString(), actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public async Task Should_Return_Books_With_Default_Order()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedTotalCount = _data.Count(x => !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
            var expectedItemsCount = _data.Count(x => !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
            var expectedFirstItemId = 1;
            // act
            var book = await booksDataService.GetBooks(null, null, String.Empty, String.Empty);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedTotalCount, book.TotalCount);
            Assert.AreEqual(expectedItemsCount, book.Items.Length);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }

        [TestCase("title_asc", 9)]
        [TestCase("publisher_asc", 5)]
        [TestCase("language_asc", 3)]
        [TestCase("isbn_asc", 3)]
        [TestCase("description_asc", 30)]
        [TestCase("quantity_asc", 5)]
        [TestCase("paperback_asc", 29)]
        [TestCase("publishdate_asc", 10)]
        [TestCase("createddate_asc", 2)]
        [TestCase("modifieddate_asc", 2)]
        [TestCase("title_desc", 7)]
        [TestCase("publisher_desc", 11)]
        [TestCase("language_desc", 1)]
        [TestCase("isbn_desc", 9)]
        [TestCase("description_desc", 11)]
        [TestCase("quantity_desc", 1)]
        [TestCase("paperback_desc", 3)]
        [TestCase("publishdate_desc", 2)]
        [TestCase("createddate_desc", 3)]
        [TestCase("modifieddate_desc", 3)]
        public async Task Should_Return_Books_With_Specified_Order(string order, long expectedFirstItemId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act
            var book = await booksDataService.GetBooks(null, null, order, String.Empty);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }


        [TestCase("title_asc", 10)]
        [TestCase("publisher_asc", 6)]
        [TestCase("language_asc", 3)]
        [TestCase("isbn_asc", 3)]
        [TestCase("description_asc", 6)]
        [TestCase("quantity_asc", 6)]
        [TestCase("paperback_asc", 6)]
        [TestCase("publishdate_asc", 10)]
        [TestCase("createddate_asc", 10)]
        [TestCase("modifieddate_asc", 10)]
        [TestCase("title_desc", 3)]
        [TestCase("publisher_desc", 10)]
        [TestCase("language_desc", 6)]
        [TestCase("isbn_desc", 6)]
        [TestCase("description_desc", 3)]
        [TestCase("quantity_desc", 3)]
        [TestCase("paperback_desc", 3)]
        [TestCase("publishdate_desc", 6)]
        [TestCase("createddate_desc", 3)]
        [TestCase("modifieddate_desc", 3)]
        public async Task Should_Return_Books_With_Specified_Order_Which_Contains_Text(string order, long expectedFirstItemId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedTotalCount = 3;
            var expectedItemsCount = 3;
            var searchString = "Small";

            // act
            var book = await booksDataService.GetBooks(null, null, order, searchString);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedTotalCount, book.TotalCount);
            Assert.AreEqual(expectedItemsCount, book.Items.Length);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }

        [TestCase(1,0,1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 3)]
        [TestCase(1, 3, 4)]
        [TestCase(1, 4, 5)]
        [TestCase(1, 5, 6)]
        [TestCase(1, 6, 7)]
        [TestCase(1, 7, 8)]
        [TestCase(1, 8, 9)]
        [TestCase(1, 9, 10)]
        public async Task Should_Return_Books_With_Offset_And_Count(int count, int offset, int expectedFirstItemId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedTotalCount = 13;
            var expectedItemsCount = 1;

            // act
            var book = await booksDataService.GetBooks(count, offset, String.Empty, String.Empty);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedTotalCount, book.TotalCount);
            Assert.AreEqual(expectedItemsCount, book.Items.Length);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }
    }
}
