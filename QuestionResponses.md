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

## · Describe a design pattern you’ve used in production code. What was the pattern? How did you use it? Given the same problem how would you modify your approach based on your experience?
