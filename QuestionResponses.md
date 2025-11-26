## · Please describe the differences between IAAS, PAAS and SAAS and give examples of each in a cloud platform of your choosing?
IAAS - Infrastructure-as-a-Service - all the layers in a cloud platform that are related to hardware, networking infrastructure, storage capabilities, virtualization and managed by the cloud provider. With IAAS customer needs to manage the platform layer, applications and data. Examples in Azure: Azure VMs, Azure Load Balancer, Azure VNETs.  
PAAS - Platform-as-a-service - cloud provider manages infrastructure (networking, hardware, virtualization) and the platform layer (operating system, middleware and runtime). Customer manages software - applications and data. Examples in Azure: Azure App Service, Cosmos DB, Azure functions.<br>
SAAS - Software-as-a-service - cloud provider manages infrastructure, platform and software, and customer is just using the application. Examples in Azure: Microsoft 365, Dynamics 365.<br>

## · What are the considerations of a build or buy decision when planning and choosing software?
When deciding whether to buy software or build it, these considerations should be taken into account:  
 Cost - the costs of building and subsequent maintaining of the software need to be weighed against the purchase price, licensing costs, future support costs and customization expenses.  
 Time - how long will it take to design, build and deploy own implementation vs. acquiring a commercial solution  
 Software requirements - How unique are requirements for the needed software? If requirements are very special, building custom software might be beneficial, otherwise purchasing a standard commercial solution might make more sense.  
 Development resources - is there sufficient planning, development, and operations talent to build, deploy and maintain the software?  
 Provider reliability - if considering to buy, will the provider be able to support the software for the needed duration?  
 Security - available commercial software should come with security certifications; implementing it on your own might be difficult  
 Compliance - available commercial software should come with compliance certifications; implementing it on your own might be difficult  
 Integration complexity - if building, software can be designed to properly integrate with existing environment; if buying, need to make sure that integration is feasible

## · What are the foundational elements and considerations when developing a serverless architecture?
Foundational elements:  
FAAS (Function-as-a-Service) Compute - event-driven functions which are short-lived and auto-scaled  
Event-driven design - system should be designed around event sources that trigger code execution - HTTP requests, API requests, database changes, file updates  
Stateless design - each function invocation should be stateless  
API layer - managed API gateway that routes requests to functions  
Serverless storage - Object/blob storage, NoSQL databases, caching  
Authentication and Authorization - identity provider which enables authentication and authorization enforcement  
Observability - logs and metrics  

Considerations:  
Statelessness - serverless functions need to be stateless at all times  
Function granularity and responsibility - functions should be single-purpose  
Cold starts - functions that are not used frequently may have delayed start times  
Idempotency - functions need to be idempotent (same input - same effect)  
Security - least-privileged identity access management roles for each function, secrets should be stored in cloud vault  
Cost - for serverless, costs directly scale with usage as billing is normally per-invocation and duration 
Testing and local development - unit tests, integration testing, contract testing  
Deployment and operations  
Error handling  
Vendor lock-in  

## · Please describe the concept of composition over inheritance
Composition over inheritance is a core design principle in object-oriented programming that states: favor object composition instead of class inheritance when building flexible, maintainable, and reusable systems. The main idea is as follows: instead of making a class inherit behavior from a parent class (is-a relationship), give it the behavior by containing an instance of another class (has-a relationship). Composition is preffered due to loose coupling, no fragile base class, flexible structure. With composition, behavior is reused via interfaces instead of reusing implementation in inheritance. With composition, it is easier to swap or extend behavior later than with inheritance.

## · Describe a design pattern you’ve used in production code. What was the pattern? How did you use it? Given the same problem how would you modify your approach based on your experience?  
I've used Strategy pattern to load and present to the user a human-interaction-verification challenge depending on the signup scenario/flow. The interface for the strategy defined the methods that can be performed with the challenge (e.g. Create new challenge, Solve challenge), and each particular challenge type (e.g. Captcha, SMS) implemented the interface. Various signup flow types would specify the challenge type that should be used at flow initialization time, and then the logic responsible for invoking and validating the challenge would call the corresponding method for the challenge and the correct implementation for the challenge type would be executed. The pattern worked perfectly for our needs, and I would not modify this implementation with any other approach.
