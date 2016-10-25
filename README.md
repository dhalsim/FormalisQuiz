FormalisQuiz
============
web.config üzerinde FormalisQuizContext connection stringi ayarlanır,
ardından IntegrationTests altındaki testler çalıştırılarak db ve tablolar, dummy veriler yaratılabilir.
Bunun için bilgisayarınızda NUnit kurulması gerekiyor, packages\NUnit.Runners.2.6.2\tools altından 
NUnit.exe ile de çalıştırılabilir.

Ya da Package Manager Console üzerinden Enable-Migrations ve Update-Migrations komutlarıyla admin kullanıcısı eklenebilir:

DB kurulumundan sonra proje çalıştırıldıktan sonra karışınıza giriş ekranı gelecektir.
Admin kullanıcısı ve parolası:

testUser
password

Admin kullanıcısıyla login olup, yeni kullanıcı ve test giriniz. Ardından bu testi kullanıcıyla eşleştiriniz. Yeni kullanıcıyla login olduğunuzda ana sayfada o testi alabilirsiniz.

Testi tamamladığınızda kaç soruyu doğru cevapladığınız ana sayfada gösterilir.