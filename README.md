# Decorator Pattern

This is a structural design pattern, that allows an object to be dynamically enhanced with new behaviors without changing the original behavior.

## How it works

The decorator object wraps the original class, containing new behaviors, like the diagram below:

![Flow](./.assets/flow.png)

## About this project

In this case, we encapsulate the logger works, using the Clean Code strategies and the Decorator Pattern. 
When you want to logger a repository functionality, you going to call the Decorator Repository, informing the original Component

### Participants

* ConcreteComponent <sub>(UserRepository)</sub> - the original class that contains some behaviors
* Component <sub>(IUserRepository)</sub> - the interface that your original class implements
* ConcreteDecorator <sub>(LoggerRepository)</sub> - The class that extends of a Component's interface to use it and include some new behaviors
* Decorator <sub>ILoggerRepository</sub> - the interface that your ConcreteDecorator implements

### Let's the Code

## Pros and Cons

+ Allow to add new behaviors without changing the original code
+ Easy to understand
+ You don't need to use inheritance every time

- Complexity to debug
- Complexity when the decorator interfaces is big
