# Breeze
Breeze is a sleek and intuitive weather app designed to provide real-time, hyper-local forecasts with ease. Whether you're planning your day or vacation, tracking multiple locations, or staying ahead of severe weather alerts, Breeze delivers accurate and up-to-date information in a clean, user-friendly interface. With personalized recommendations and seamless updates, staying informed about the weather has never been this effortless.

## PRODUCT VISION
**FOR** individuals who spend a lot of time outdoors  
**WHO** want accurate, real-time whather updates for any part of Romania to plan their daily activities easily. (nume) is a smart weather platform   
**THAT** provides hyper-local forecasts, severe weather alerts, and tailored recommendations for outdoor plans, ensuring users stay informed and prepared.  
**UNLIKE** generic weather apps that focus only on basic forecasts, we offer a personalized, context-aware experience that adapts to users' locations, interests, and routines.  
**OUR PRODUCT** delivers real-time weather insights, activity-based recommendations, and a sleek, intuitive interface—helping users make the most of every day, no matter the weather.  

## PRODUCT FEATURES
### User Registration
•	Allow users to sign up for access to the site.  
•	Display a confirmation pop-up after successful registration.  
### User Authentication
•	Allow users to log in and access the platform.  
### Real-Time Weather Updates
•	Get live temperature, humidity, wind speed, and precipitation levels.  
### Hyper-Local Forecasts
•	Hourly and daily forecasts based on precise location in Romania.  
•	Neighborhood-level accuracy using ANM’s APIs.  
### Multi-Location Support
•	Save and track multiple locations for quick access to different city forecasts.  
•	Useful for travelers, remote workers, and frequent commuters. 
### Week-Ahead Weather
•	Get accurate daily forecasts for the next five days, including temperature trends, precipitation chances, and wind conditions

## User Stories 
### User Registration and Authentication
•	As a new user, I want to sign up for an account so that I can access personalized weather updates and save my locations  
•	 As a user, I want to see a pop-up that confirms the registration so I know that it was a success.  
•	 As a user, I want to log in so I can access the platform.  
### Real-Time Weather Updates
•	As a user, I want to see live weather conditions so that I can plan my activities accordingly.  
•	As a user, I want the data to refresh automatically at regular intervals.  
### Hyper-Local Forecasts
•	As a user, I want to receive accurate weather forecasts for my exact location so that I can prepare for the day ahead   
•	As a user, I want to be able to view daily forecasts.  
### Multi-Location Support
•	As a user, I want to save and track weather for multiple locations so that I can stay informed about the weather in different places.  
•	As a user, I want to quickly switch between saved locations to view forecasts.  
### Week-Ahead Weather
•	As a user, I want to be able to check the forecast for multiple days so that I can plan for the comming week.

## User Scenarios
### Scenario 1: Alex, dedicated corporate worker, 31 years old
Alex wakes up early, groggy but knowing he needs to check the weather before heading out. He grabs his phone and opens Breeze, which immediately shows the current temperature, humidity, and wind speed based on his location. Scrolling down, he notices rain is expected in the afternoon. He sighs, grabs an umbrella, and checks the hourly forecast to see exactly when the rain will start.
Since it’s Wednesday, he also takes a quick peek at the weekend forecast—perfect weather for a hiking trip he’s been planning. Smiling, he adds a reminder to his calendar and saves his office location in the app to keep track of the weather during his commute. With everything set, he heads out, feeling prepared for the day ahead.

### Scenario 2: Maria, travel enthusiast, 23 years old
Maria zips up her suitcase and sits down to do one final check before heading to the airport. Opening Breeze, she navigates to "Saved Locations" and quickly adds Paris, her destination. Within seconds, a detailed five-day forecast appears, showing temperatures much colder than back home. She frowns, realizing she needs to pack a heavier jacket and an umbrella.
As her flight takes off, Maria keeps an eye on both her hometown and Paris, switching between the two locations with ease. Later that evening, while sipping coffee at her hotel, she notices that severe weather is expected in Paris tomorrow. She enters the app, sees a storm warning, and decides to adjust her plans. Thanks to Breeze, she’s one step ahead of the unpredictable weather.

### Scenario 3: Daniel, cycling amateur, 46 years old
Daniel straps on his cycling helmet, ready to hit the road for a long ride through the countryside. The sky is clear, but he knows the weather can change quickly. He pulls up Breeze and checks the forecast for his route. Everything looks good, though there’s a slight risk of thunderstorms in the evening.  A few hours into his ride, while on a break, he checks his phone —a storm warning for his exact location. Heart racing, he quickly opens the radar map and sees dark clouds approaching fast. Without hesitation, he reroutes to the nearest shelter just as heavy rain starts to fall. As he watches the storm from a café window, he feels relieved. Without Breeze’s real-time updates, he would’ve been caught miles from safety.

## Backlog
| **Sprint** | **Goal** | **Tasks** |
|------------|----------|-----------|
| **🟢 Sprint 1** <br> (User Authentication & Basic Setup) | Implement user authentication so users can register and log in. | - Set up a **user database** . <br> - Implement **user registration and login** (email & password authentication). <br> - Create **basic UI** for sign-up/login pages. <br> - Display a **confirmation pop-up** after successful registration. |
| **🟠 Sprint 2** <br> (Real-Time Weather Updates) | Enable live weather data retrieval and automatic updates. | - Integrate with a **weather API** (ANM API). <br> - Display **real-time weather data** (temperature, humidity, wind speed, precipitation). <br> - Implement **automatic data refresh** (e.g., every 30 minutes). <br> - Design the **weather dashboard UI**. |
| **🟡 Sprint 3** <br> (Hyper-Local Forecasts) | Provide accurate weather forecasts based on user location. | - Fetch **hourly and daily forecasts**. <br> - Display **a 7-day forecast** section in the UI. <br> - Improve API efficiency by caching requests for **faster loading**. |
| **🔵 Sprint 4** <br> (Multi-Location Support & UI Enhancements) | Allow users to save and switch between multiple locations. | - Add functionality to **save user’s preferred locations**. <br> - Implement **a dropdown or quick-access UI** for switching locations. <br> - Enable **real-time weather updates for multiple saved locations**. <br> - Polish the **UI and responsiveness** for a better user experience. |

## C4 Diagrams
### Context diagram
![alt text](https://github.com/unibuc-cs/software-engineering-product-5togo/blob/documentation/Diagrams/structurizr-Context.png)
### Container diagram
![alt text](https://github.com/unibuc-cs/software-engineering-product-5togo/blob/documentation/Diagrams/structurizr-Container.png)
### Component diagram
![alt text](https://github.com/unibuc-cs/software-engineering-product-5togo/blob/documentation/Diagrams/structurizr-Component.png)

# Architectural report
Overall, the app ended up similar to what we envisioned at the halfway point in this project, but unfortunately the result is less complex than what we wanted when we started. Breeze successfully delivers accurate and real-time weather updates with a robust and scalable architecture. Built with Angular for the frontend and .NET for the backend, the application ensures smooth performance and a modern user experience. Through rigorous testing, security measures, and a well-defined CI/CD pipeline, the application meets both functional and non-functional requirements.

## Non-functional requirements
### Usability
- The system is designed with the user in mind, providing an intuitive and user-friendly interface that makes it easy for users to navigate and interact with the application.
### Performance
- The API response time is consistently under 2 seconds to ensure an optimal user experience for all interactions.  

- The system is capable of supporting up to 1000 concurrent users without experiencing any significant latency, ensuring smooth performance even under heavy load.  
### Mentainability
- The system’s codebase is modular and follow clean coding practices, ensuring that it is easy to maintain, debug, and extend over time.

- Comprehensive documentation is provided for both the frontend and backend systems, enabling new developers to quickly understand the system’s architecture and contribute to its development.

- The system includes logging and monitoring capabilities to facilitate the identification of issues and ensure quick troubleshooting of problems in production.

## Quality Assurance (QA)
### Acuratețea datelor meteo
**Obiectivul Testării**: Verificarea corectitudinii și actualizării datelor meteo furnizate de API.  
**Momentul Testării**: Testare și Mentenanță.  
**Metoda Testării**: Compararea datelor API cu surse de referință (Black Box Testing), verificarea erorilor de preluare a datelor.  
**Rezultate**: Datele meteo sunt corecte, dar pot exista discrepanțe minore între surse.  
### Corectitudinea datelor utilizatorilor
**Obiectivul Testării**: Validarea corectitudinii și securizării preferințelor utilizatorilor (ex. locația salvată, unități de măsură).  
**Momentul Testării**: Dezvoltare, Testare și Analiză.  
**Metoda Testării**: Verificarea fluxului de date între frontend și backend (Integration Testing), verificarea manuală a setărilor utilizatorilor.  
**Rezultate**: Datele utilizatorilor sunt stocate și recuperate corect.  
### Performanța aplicației
**Obiectivul Testării**: Evaluarea timpului de răspuns pentru afișarea datelor meteo și a timpului de încărcare al aplicației.  
**Momentul Testării**: Testare și Mentenanță.  
**Metoda Testării**: Măsurarea timpilor de răspuns ai API-ului și performanța UI în diferite condiții de rețea.  
**Rezultate**: Aplicația funcționează bine.  
### Funcționalitatea generală
**Obiectivul Testării**: Testarea tuturor fluxurilor de utilizare, inclusiv căutarea unui oraș și afișarea prognozei.  
**Momentul Testării**: Testare și Implementare.  
**Metoda Testării**: Unit Tests pentru verificarea funcțiilor critice, testarea manuală a UI/UX.  
**Rezultate**: Funcționalitatea aplicației este stabilă și fără erori majore.  
### UX/UI
**Obiectivul Testării**: Verificarea experienței utilizatorului pentru o navigare intuitivă și ușor de utilizat.  
**Momentul Testării**: Design, Testare și Implementare.  
**Metoda Testării**: Evaluarea interacțiunii utilizatorilor, feedback de la testeri reali.  
**Rezultate**: Aplicația este ușor de folosit, dar unele elemente UI pot necesita optimizare.  



## Security Analysis
### Security Risks
- One of the key security risks identified is API key exposure, which could allow unauthorized access to the weather API and lead to data breaches or misuse.

- Another risk is that of a SQL injection, a critical security risk where attackers can manipulate database queries by injecting malicious SQL code, potentially leading to unauthorized data access or manipulation.

- Another risk is potential data leakage, where sensitive user preferences could be exposed due to improper handling or storage of user data.


### Security Measures
- To mitigate the risk of API key exposure, all API keys are securely stored in environment variables, ensuring they are never exposed in frontend code or accessible to unauthorized parties.

- All sensitive data, including user information, are encrypted, ensuring that data cannot be easily intercepted or accessed by unauthorized individuals.

- We used Entity Framework, an Object-Relational Mapping (ORM) framework for .NET that simplifies database interactions by allowing developers to work with data as objects, rather than writing raw SQL queries. It enhances security by preventing SQL injection through parameterized queries, automatically sanitizing user inputs. The framework also ensures data validation before it is persisted, preventing invalid or malicious data from being stored


## CI/CD 
We utilized GitHub for version control, enabling seamless collaboration and code management. All source code is stored in a centralized GitHub repository, allowing us to push changes, create branches, and submit pull requests for review. Once a pull request is approved, the code is merged into the main branch, which triggers deployment to the appropriate environment.  
Also, we used GitHub Actions to set up a CI/CD pipeline that automates the build and testing process for our project. The pipeline is triggered on every push and pull request to the main branch, ensuring that new code is always validated before being merged.


## Demo
- [Here](https://youtu.be/xX8Tc6Y2ZXM) you have a demo of the project.
