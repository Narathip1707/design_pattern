# Decorator Pattern - Streaming Subscription System

## Overview
This project demonstrates the Decorator Pattern with a streaming subscription system.
The pattern adds features (HD, Full HD, Ultra HD, No Ads) dynamically without changing the base class.

---

## Class Diagram

```mermaid
classDiagram
	%% Component
	class ISubscription {
		<<interface>>
		+GetDescription() string
		+CalculatePrice() double
		+GetOwner() string
	}

	%% Concrete Component
	class BasicPlan {
		-double price
		-string resolution
		-string ownerName
		+BasicPlan(name)
		+GetDescription() string
		+CalculatePrice() double
		+GetOwner() string
	}

	%% Base Decorator
	class SubscriptionDecorator {
		<<abstract>>
		-ISubscription subscription
		+SubscriptionDecorator(s)
		+GetDescription() string
		+CalculatePrice() double
		+GetOwner() string
	}

	%% Concrete Decorators
	class NoAdsDecorator {
		+NoAdsDecorator(s)
		+GetDescription() string
		+CalculatePrice() double
	}

	class HD_Decorator {
		+HD_Decorator(s)
		+GetDescription() string
		+CalculatePrice() double
	}

	class FullHD_Decorator {
		+FullHD_Decorator(s)
		+GetDescription() string
		+CalculatePrice() double
	}

	class UltraHD_Decorator {
		+UltraHD_Decorator(s)
		+GetDescription() string
		+CalculatePrice() double
	}

	%% Client (Program)
	class Program {
		+Main(args)
		+Display(sub)
	}

	%% Relationships
	ISubscription <|.. BasicPlan : implements
	ISubscription <|.. SubscriptionDecorator : implements
	SubscriptionDecorator <|-- NoAdsDecorator : extends
	SubscriptionDecorator <|-- HD_Decorator : extends
	SubscriptionDecorator <|-- FullHD_Decorator : extends
	SubscriptionDecorator <|-- UltraHD_Decorator : extends
	SubscriptionDecorator o-- ISubscription : wraps
	Program --> ISubscription : uses
```

---

## Decorator Pattern Components

### 1) Component
- ISubscription (interface)
  - Defines the common contract: GetDescription, CalculatePrice, GetOwner.

### 2) Concrete Component
- BasicPlan
  - Base subscription with a default price and resolution.

### 3) Decorator (Base)
- SubscriptionDecorator
  - Holds a reference to ISubscription and forwards calls.

### 4) Concrete Decorators
- NoAdsDecorator: adds no-ads feature and extra cost.
- HD_Decorator: adds HD feature and extra cost.
- FullHD_Decorator: adds Full HD feature and extra cost.
- UltraHD_Decorator: adds 4K feature and extra cost.

### 5) Client
- Program
  - Composes subscriptions at runtime by wrapping decorators.

---

## Why This Is Correct Decorator Usage
- Features are added by wrapping, not by subclass explosion.
- Each decorator preserves the ISubscription interface.
- Combinations are created dynamically (order and selection are flexible).

---

## Example Usage

```csharp
ISubscription basePlan = new BasicPlan("Narathip");
ISubscription planWithHD = new HD_Decorator(basePlan);
ISubscription hdNoAdsPlan = new NoAdsDecorator(planWithHD);
```

```csharp
ISubscription premiumPlan = new UltraHD_Decorator(
	new NoAdsDecorator(new BasicPlan("Somsak"))
);
```
