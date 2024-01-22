## Onion Architecture
  Katmanların birbirleri ile olan ilişkilerine odaklanan bu mimaride .net core projesi içerisinde çok sık kullanılan IoC tasarımını ve DI konseplerini uygulayarak uygulamamızı gerçekleştireceğiz. Bu konsept ile
  bağımlılıklarımızı dışarıdan içeriye olacak şekilde oluşturacağız ve bu sayede komponentlerin birbirleri ile gevşek bağlı olduğu bir mimari oluşturacağız.

## IoC (Inversion of control)
  Bu tasarım nesnelerin yaşam döngüsünü ve bağımlılıklarını dış bir ortamda (konteyner:program.cs) kontrol etmemizi sağlar. Bunun örneklerini projede program.cs dosyası içersinde daha net göreceğiz. Bu tasarımı
  kullanırken DI konseptlerini ele alacağız.

## Dependency Injection,Inversion
  Dependency inversion nesnelerin arasındaki bağları soyutlayarak bağımlılıkları düşürmeyi amaçlayan bir prensiptir. Dependency injection ise bunu uygulamamızı sağlayan bir tekniktir. Bunu ise interfaceler
  aracılığı ile yapmamıza ve bu sayede her yerde new keyword'u kullanarak nesne oluşturmak yerine IoC'de bahsettiğimiz konteyner'a inject ederek birbirleri ile gevşek bağlı komponentler oluşturmamızı sağlar.

## Proje Klasör Hiyerarşisi
- Common : Cross-cutting concern farklı katmanlarda yer alan ve ortak kullanılan yapıları bu katmanda ele alıyoruz. (örn:loglama)
- Core : Uygulamamızın business konularını bu katmanda oluşturuyoruz.
  - Application : Core business konularını aggregate'ler ve CQRS ile ele aldığımız katman.
  - Domain : Business entitylerimizin yer aldığı katman.
- Infrastructure : Uygulamamızı dışarıdaki veritabanlarına veya dışarıdaki ağlara açtığımız katman
  - Persistance : Veritabanı işlemlerini bu katmanda yönetiyoruz.
- Presentation
  - Web Api : UI ile erişimimizi sağlayan controller'ların yer aldığı katman.
  - Middleware : Bu katmanda validation, exception handling, auth gibi request-response pipeline içerisinde kalan konuları ele alıyoruz.
 

![Klasör](https://github.com/mackali1453/OnionArchitecture/assets/87720632/32f19be7-c299-4f19-8c87-212e44edd200)


Bağımlılık sırasına göre katmanlar ve bahsedilmesi gerek önemli konular şunlardır,

## Presentation
  - Web Api : Burada bahsedilmesi gereken en önemli konu IoC ve DI ile nesnelerin yaşam döngüsünü kontrol ettiğimiz program.cs dosyası.
  ```
    builder.Services.AddCommonDependencies();
  ```
  Bu kod satırında extension metod aracılığı ile ilgili katmana gidip bağımlılıklarımızı konteyner içine inject ediyoruz.
  
  ```
    public static void AddCommonDependencies(this IServiceCollection builder)
    {
        builder.AddScoped(typeof(IServiceLogger<>), typeof(ServiceLogger<>));           
    }
  ```
  - Middleware : Mevcuttaki projede middleware üzerinden 2 tane konuyu ele alacağız. Biri exception handling diğeri ise validation. Middleware'ın bize sunduğu diğer avantaj ise DRY ile ele alınabilir.
    Sadece apiler'imize eklediğimiz attribute'ler aracılığı ile her yerde tekrarlanan kod satırlarından kurtulabiliriz.
    
    - Exception Handling : LoggingAttribute yardımı ile request,response ve exception'larımızı attribute aracılığı ile database'e logluyoruz. Bunu custom olarak oluşturduğunuz ServiceLogger aracılığı ile yapıyoruz.
    - Validation : Custom validation sınıflarımızı reflection yardımı ile runtime instance alıp validasyon işlemlerini gerçekleştiriyoruz. Burada Fluent Validation kütüphanesinden yardım alıyoruz. 
    Peki sınıf ismini bilmeden nasıl instance alıyoruz derseniz aşağıdaki kod bloğunda bunu daha net anlayabiliriz,
    ```
    private void GetInstance(ActionExecutingContext context, object? model)
        {
            try
            {
                var validatorType = typeof(GenericValidator<>).MakeGenericType(model.GetType());
                ...
      
    ```
    ```
            public class GenericValidator<T> : AbstractValidator<T>
            {
      
    ```
    Kod satırında görüleceği üzere validasyon işlemini gerçekleştirdiğimiz bütün metodlar generic GenericValidator<T> kalıtım almaktadır. Reflection kütüphanesinin bize sunduğu generic sınıflardan kalıtım alan sınıflardan instance
    alma kolaylığı ile validation işlemlerini DRY'ı çiğndemeden attribute araclığı ile yapıyoruz.

 ## Infrastructure
   - Persistance : Bu katmanda veritabanı işlemlerini yönetiyoruz. Burada bahsedilmesi gereken önemli detaylar şunlardır,
      - Unit of Work : Bu pattern aracılığı ile transaction işlemlerimizi bir bütün olarak ele almamızı sağlar. Bunun bize birden çok faydası vardır. Bunların biri generic repository ile kullanımda repositoryleri new keyword'u ile instance
        almak yerine unit of work sınıflarımız aracılığı ile ele alabilmemizdir. 
      
        ```
        public class Uow : IUow
        {
            private readonly WebApiContext _context;
            private readonly LogContext _logContext;
    
            public Uow(WebApiContext context)
            {
                _context = context;
            }
            public IRepository<T> GetRepository<T>() where T : class, new()
            {
                return new Repository<T>(_context);
            }
            public IRepository<T> GetLogRepository<T>() where T : class, new()
            {
                return new Repository<T>(_logContext);
            }
            public IUserRepository<AppUser> GetUserRepository()
            {
                return new UserRepository<AppUser>(_context);
            }
            public async Task SaveChanges()
            {
                await _context.SaveChangesAsync();
            }
            public async Task SaveLogChanges()
            {
                await _logContext.SaveChangesAsync();
            }
        }
        ```
        
         Bir diğer avantajı ise transaction işlemlerini bir bütün olarak ele alabilmemizdir. Buradaki fayda herhangi bir transaction işleminde bir hata meydana geldiğinde bir önceki işlemlerde SaveContext yapmadığımız için rollback işlemini gerçekleştirebiliriz.
        
  
         ```
                var userRepository = _iUow.GetRepository<AppUser>();
                var roleRepository = _iUow.GetRepository<AppUserRole>();
    
                await userRepository.CreateAsync(new AppUser
                {
                    UserName = request.Username,
                    Password = request.Password,
                });
    
                Expression<Func<AppUser, bool>> condition = person => person.UserName == request.Username && person.Password == request.Password;
    
                var createdPerson = await userRepository.GetByFilter(condition);
                await roleRepository.CreateAsync(new AppUserRole { RoleId = (int)RoleTypes.Member, UserId = createdPerson.UserId });
    
                await _iUow.SaveChanges();
          ```
      
          Bu örnekte görebileceğimiz üzere bir kullanıcı register olurken önce kullanıcı oluşturuyoruz ve ardından ona rol ataması yapıyoruz. Unit of work pattern kullanmasaydık(her command işleminden sonra SaveChanges kullanmak) kullanıcı oluştuktan 
          sonra rol ataması yapılırken meydana gelen bir transaction hatasında rolü olmayan bir kullanıcı oluşturmuş olacaktık ve bu istenmeyen bir durumdur. Ama yukarıdaki örnekte SaveChanges ile bütün işlemleri bir bütün olarak ele almış olduk.

     - Log ve Diğer contextleri birbirinde ayırmak : Buradaki senaryo exception handling ederken karşımıza çıkacaktır. Örneğin bir transaction işleminde exception fırlatıldı ve loglarken catch bloğunda bunu yakaladık. İlgili logu SaveChanges
      ile database yazmaya kalktığımızda tek bir context ile çalıştığımız zaman zaten bir önceki transaction işlemi hata fırlattığı için ilgili context loglamayı yapmayacaktır. Bağımsız contextler ile çalıştığımız zaman ise fırlatılan exceptionlar
      ayrı contextlerde yer alacağı için loglama işlemlerini etkilemeyecektir.

## Core
- Application : Bu katman core business işlemlerini gerçekleştirdiğimiz katmandır. Infrastructure katmanında oluşturduğunuz unit of work patternini burada uygulamaya geçireceğiz ve ilgili entitylerde veritabanı işlemleri gerçekleştireceğiz.
      Bu işlemleri yaparken CQRS patterni çok işimize yarayacaktır. Özetle komut ve sorgu sorumluluklarını ayırmaya yarayan bu pattern Domain Driven Design terimi olan aggregate ile iç içedir. Bu ikisi bizlere domain entitylerimizi bir bütün
      olarak ele almamızı sağlar. Klasör hiyerarşisine baktığımızda bunu daha iyi anlıyor olacağız.

![aggregate](https://github.com/mackali1453/OnionArchitecture/assets/87720632/91f47e2f-6161-4aa5-9397-76888198aa01)
