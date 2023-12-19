import './App.css';
import './Website.css'

export default function Main() {
    
    const serviceName = "Pomoycar"
    const aboutService = "Сервис онлайн записи на автомойки"
    
    return ( 
      <div className="App">
        <header className='header'>
          <h1 className='header__title'>{serviceName}</h1>
        </header>
        <article className="article article_whitebg article_first">
          <h2 className='black'>{serviceName}</h2>
          <h3>{aboutService}</h3>
          <p className='black'>
            Сервис предоставляет пользователям удобный и современный формат записи на автомойку
            <br/>
            Приложение доступно для устройств на Android 
            <br/>
            На данный момент сервис запущен в городе <strong>Томск</strong>
          </p>
        </article>
      
        <article className="article article_bluebg article_second">
          <h2 className="white">О нас</h2>
          <p className="white">
              Россия, г. Томск, Second Self Inc.
          </p>
          <p className="white">
            По всем вопросам: <a className="white" href="mailto:support@pomoycar.ru">support@pomoycar.ru</a>
          </p>
          <p className="white">
            <a className="white" href="/privacy">Политика конфиденциальности</a>
          </p>
          
          <p className="white">
            <a className="white" href="/terms">Условия использования</a>
          </p>
        </article>
    </div>
    );
}
